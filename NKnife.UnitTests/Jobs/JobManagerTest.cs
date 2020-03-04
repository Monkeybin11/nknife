﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NKnife.Interface;
using NKnife.Jobs;
using Xunit;

namespace NKnife.UnitTests.Jobs
{
    public class JobManagerTest
    {
        private class Job : IJob
        {
            public int Id { get; set; }
            public bool IsPool { get; } = false;
            public int Timeout { get; set; }
            public bool IsLoop { get; set; }
            public int Interval { get; set; }
            public int LoopCount { get; set; }
            public int CountOfCompleted { get; set; }
            public Func<IJob, bool> Run { get; set; }
            public Func<IJob, bool> Verify { get; set; }
        }

        private class JobPool : List<IJobPoolItem>, IJobPool
        {
            public bool IsPool { get; } = true;

            #region Implementation of IJobPool

            /// <summary>
            /// 工作池中的子工作轮循模式，当True时，会循环执行整个池中的所有子工作；当False时，对每项子工作都会执行完毕，才执行下一个工作。
            /// </summary>
            public bool IsOverall { get; set; } = false;

            #endregion
        }

        private int _number = 0;

        private bool CountFunc(IJob job)
        {
            _number++;
            return true;
        }

        #region 001-004：基本测试

        /// <summary>
        /// 单个工作，指定循环次数
        /// </summary>
        [Fact]
        public void RunTest001()
        {
            _number = 0;
            var flow = new JobManager();
            flow.Pool = new JobPool();
            var job = new Job
            {
                IsLoop = true,
                LoopCount = 50,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc
            };
            flow.Pool.Add(job);
            flow.Run();
            _number.Should().Be(job.LoopCount);
        }

        /// <summary>
        /// 单个工作，指定循环次数
        /// </summary>
        [Fact]
        public void RunTest002()
        {
            _number = 0;
            var flow = new JobManager();
            flow.Pool = new JobPool();
            var job1 = new Job
            {
                IsLoop = true,
                LoopCount = 1,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc
            };
            var job2 = new Job
            {
                IsLoop = true,
                LoopCount = 500,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc
            };
            flow.Pool.AddRange(new IJobPoolItem[] {job1, job2});
            flow.Run();
            _number.Should().Be(job1.LoopCount + job2.LoopCount);
        }

        /// <summary>
        /// 多个工作，工作均无需循环
        /// </summary>
        [Fact]
        public void RunTest003()
        {
            _number = 0;
            var flow = new JobManager();
            flow.Pool = new JobPool();

            for (int i = 0; i < 300; i++)
            {
                var job = new Job
                {
                    IsLoop = false,
                    Interval = 2,
                    Timeout = 15,
                    Run = CountFunc
                };
                flow.Pool.Add(job);
            }
            flow.Run();
            _number.Should().Be(300);
        }

        /// <summary>
        /// 三个工作，第1工作，第3工作无需循环，第2工作指定循环次数
        /// </summary>
        [Fact]
        public void RunTest004()
        {
            _number = 0;
            var flow = new JobManager();
            flow.Pool = new JobPool();
            var job1 = new Job
            {
                IsLoop = false,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc
            };
            var job2 = new Job
            {
                IsLoop = true,
                LoopCount = 100,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc
            };
            var job3 = new Job
            {
                IsLoop = false,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc
            };
            flow.Pool.AddRange(new []{job1,job2,job3});
            flow.Run();
            _number.Should().Be(102);
        }

        #endregion

        #region 005：测试中止功能

        private readonly JobManager _runTest005Manager = new JobManager();
        
        private bool OnBreakCountFunc(IJob job)
        {
            _number++;
            if (_number >= 100)
                _runTest005Manager.Break();
            return true;
        }

        /// <summary>
        /// 测试中止功能。一个无限循环的工作，当完成到100项时，中止。
        /// </summary>
        [Fact]
        public void RunTest005()
        {
            _number = 0;
            var job2 = new Job
            {
                IsLoop = true,
                LoopCount = 0,
                Interval = 2,
                Timeout = 15,
                Run = OnBreakCountFunc
            };
            _runTest005Manager.Pool = new JobPool();
            _runTest005Manager.Pool.Add(job2);
            _runTest005Manager.Run();
            _number.Should().Be(100);
        }

        #endregion

        #region 006：测试暂停与继续功能

        #region 006

        private readonly JobManager _runTest006Manager = new JobManager();
        private int _count006 = 0;
        private int _number006 = 0;

        private bool OnPauseCountFunc(IJob job)
        {
            _number006++;
            if (_number006 % 10 == 0)
            {
                _runTest006Manager.Pause();
                _runTest006Manager.Resume();
                _count006++;
            }
            return true;
        }

        /// <summary>
        /// 006：测试暂停与继续功能。一个无限循环工作，当完成到10的倍数时，暂停，直到完成100项。
        /// </summary>
        [Fact]
        public void RunTest006()
        {
            _count006 = 0;
            _number006 = 0;
            var job = new Job
            {
                IsLoop = true,
                LoopCount = 100,
                Interval = 2,
                Timeout = 15,
                Run = OnPauseCountFunc
            };
            _runTest006Manager.Pool = new JobPool();
            _runTest006Manager.Pool.Add(job);
            _runTest006Manager.Run();
            _count006.Should().Be(10);
            _number006.Should().Be(100);
        }

        #endregion

        #region 006a

        private readonly JobManager _runTest006AManager = new JobManager();
        private int _count006A = 0;

        private bool OnPauseCountFuncA(IJob job)
        {
            _count006A++;
            if (_count006A == 5)
            {
                _runTest006AManager.Pause();
            }
            return true;
        }

        /// <summary>
        /// 006：测试暂停与继续功能。一个无限循环工作，当完成到10的倍数时，暂停，直到完成100项。
        /// </summary>
        [Fact]
        public void RunTest006A()
        {
            _count006A = 0;
            var job = new Job
            {
                IsLoop = true,
                LoopCount = 20,
                Interval = 2,
                Timeout = 5,
                Run = OnPauseCountFuncA
            };
            _runTest006AManager.Pool = new JobPool();
            _runTest006AManager.Pool.Add(job);
            //另起一个线程执行，当执行计数到5的时候，暂停
            Task.Factory.StartNew(() => _runTest006AManager.Run());
            //如果暂停成功，计数器计数应该是5，而无论再等待多少时间
            Thread.Sleep(100);
            //断言测试计数器
            _count006A.Should().Be(5);

            _runTest006AManager.Resume();
            _runTest006AManager.Break();
        }
        
        #endregion

        #endregion

        #region 007：递归

        /// <summary>
        /// 测试递归：测试工作流中的工作本身就是工作组，即测试递归的有效性
        /// </summary>
        [Fact]
        public void RunTest007()
        {
            _number = 0;
            var flow = new JobManager();
            flow.Pool = new JobPool();

            // 先创建一个简单工作，加入
            var job1 = new Job
            {
                IsLoop = false,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc
            };
            flow.Pool.Add(job1);

            // 共5个工作。再创建一个复杂工作组，工作组中有5个工作，加入
            var group1 = new JobPool();
            for (int i = 0; i < 5; i++)
            {
                var job2 = new Job
                {
                    IsLoop = false,
                    Interval = 2,
                    Timeout = 15,
                    Run = CountFunc
                };
                group1.Add(job2);
            }
            flow.Pool.Add(group1);

            // 共25个工作。再创建一个复杂工作组，工作组有五个复杂工作，每个复杂工作中有5个简单工作，加入。
            var group2 = new JobPool();
            for (int i = 0; i < 5; i++)
            {
                var subGroup2 = new JobPool();
                for (int j = 0; j < 5; j++)
                {
                    var job2 = new Job
                    {
                        IsLoop = false,
                        Interval = 2,
                        Timeout = 15,
                        Run = CountFunc
                    };
                    subGroup2.Add(job2);
                }
                group2.Add(subGroup2);
            }
            flow.Pool.Add(group2);

            flow.Run();
            _number.Should().Be(31);
        }
        
        #endregion

        #region 008：事件

        /// <summary>
        /// 测试各个事件是否都被很好的触发。
        /// </summary>
        [Fact]
        public void RunTest008()
        {
            _number = 0;
            var allWorkDone = false;
            var runEvent = 0;
            var runCount = 0;
            var flow = new JobManager();
            flow.Pool = new JobPool();
            flow.AllWorkDone += (s, e) => { allWorkDone = true; };

            var job1 = new Job
            {
                IsLoop = false,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc,
            };
            var job2 = new Job
            {
                IsLoop = false,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc,
            };
            var job3 = new Job
            {
                IsLoop = false,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc,
            };
            flow.Running += (s, e) =>
            {
                runEvent.Should().Be(0);
                runEvent++;
                runCount++;
            };
            flow.Ran += (s, e) =>
            {
                runEvent.Should().Be(1);
                runEvent--;
            };
            flow.Pool.AddRange(new[] {job1, job2, job3});
            flow.Run();
            _number.Should().Be(3);
            runEvent.Should().Be(0);
            runCount.Should().Be(3);
            allWorkDone.Should().BeTrue();
        }

        #endregion

        #region 009：测试中止功能的事件

        private readonly JobManager _runTest009Manager = new JobManager();

        private bool On009BreakCountFunc(IJob job)
        {
            _number++;
            if (_number >= 100)
                _runTest009Manager.Break();
            return true;
        }

        /// <summary>
        /// 测试中止功能的事件。
        /// </summary>
        [Fact]
        public void RunTest009()
        {
            var allWorkDone = false;
            _runTest009Manager.Pool = new JobPool();
            _runTest009Manager.AllWorkDone += (s, e) =>
            {
                allWorkDone = true;//应该无法进入该事件
            };
            _number = 0;
            var job2 = new Job
            {
                IsLoop = true,
                LoopCount = 0,
                Interval = 2,
                Timeout = 15,
                Run = On009BreakCountFunc
            };
            _runTest009Manager.Pool.Add(job2);
            _runTest009Manager.Run();
            _number.Should().Be(100);
            allWorkDone.Should().BeFalse();
        }

        #endregion

        #region 010-012：成组的循环

        private int _number1 = 0;
        private int _number2 = 0;
        private int _number3 = 0;

        private bool CountFunc1(IJob job)
        {
            _number1++;
            return true;
        }
        private bool CountFunc2(IJob job)
        {
            _number2++;
            return true;
        }
        private bool CountFunc3(IJob job)
        {
            _number3++;
            return true;
        }

        /// <summary>
        /// 设定三个工作，每工作执行50次，采用工作轮循的模式
        /// </summary>
        [Fact]
        public void RunTest010()
        {
            _number = 0;
            var flow = new JobManager();
            flow.Pool = new JobPool()
            {
                //关键属性：在组内整体轮循
                IsOverall = true
            };
            var job1 = new Job
            {
                IsLoop = true,
                LoopCount = 50,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc1
            };
            var job2 = new Job
            {
                IsLoop = true,
                LoopCount = 50,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc2
            };
            var job3 = new Job
            {
                IsLoop = true,
                LoopCount = 50,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc3
            };
            flow.Pool.AddRange(new IJobPoolItem[] {job1, job2, job3});
            flow.Run();
            _number1.Should().Be(job1.LoopCount);
            _number2.Should().Be(job2.LoopCount);
            _number3.Should().Be(job3.LoopCount);
        }

        /// <summary>
        /// 设定三个工作，第一个工作无限循环，其他两个工作各执行50次，采用工作轮循的模式
        /// </summary>
        [Fact]
        public void RunTest011()
        {
            _number = 0;
            var flow = new JobManager();
            flow.Pool = new JobPool()
            {
                //关键属性：在组内整体轮循
                IsOverall = true
            };
            var job1 = new Job
            {
                IsLoop = true,
                LoopCount = 0,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc1
            };
            var job2 = new Job
            {
                IsLoop = true,
                LoopCount = 50,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc2
            };
            var job3 = new Job
            {
                IsLoop = true,
                LoopCount = 50,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc3
            };
            flow.Pool.AddRange(new IJobPoolItem[] {job1, job2, job3});
            Task.Factory.StartNew(() => flow.Run());
            Thread.Sleep(1000);//1000毫秒足够每个工作各执行多次了
            flow.Pause();
            _number1.Should().BeGreaterThan(101);
            _number2.Should().Be(50);
            _number3.Should().Be(50);
        }

        /// <summary>
        /// 设定三个工作，第一个工作无限循环，其他两个工作各执行50次，采用单个工作执行完成再继续的模式
        /// </summary>
        [Fact]
        public void RunTest012()
        {
            _number = 0;
            var flow = new JobManager();
            flow.Pool = new JobPool()
            {
                //关键属性：在组内整体轮循
                IsOverall = false
            };
            var job1 = new Job
            {
                IsLoop = true,
                LoopCount = 0,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc1
            };
            var job2 = new Job
            {
                IsLoop = true,
                LoopCount = 50,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc2
            };
            var job3 = new Job
            {
                IsLoop = true,
                LoopCount = 50,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc3
            };
            flow.Pool.AddRange(new IJobPoolItem[] { job1, job2, job3 });
            Task.Factory.StartNew(() => flow.Run());
            Thread.Sleep(500);
            flow.Pause();
            _number1.Should().BeGreaterThan(100);
            _number2.Should().Be(0);
            _number3.Should().Be(0);
        }

        #endregion

        #region 013：针对工作的间隔时间是否符合逻辑进行测试

        private const int LOOP_COUNT_13_14 = 3;
        private const int INTERVAL_13_14 = 500;
        private short _num13 = 0;
        private readonly Stopwatch _watch13 = new Stopwatch();

        private bool CountFunc13(IJob job)
        {
            if (_num13 < 1)
            {
                _num13++;
                _watch13.Reset();
                _watch13.Start();
                return true;
            }
            _watch13.Stop();
            long watchTime = _watch13.ElapsedMilliseconds;
            watchTime.Should().BeLessOrEqualTo(INTERVAL_13_14 + 2, $"【Job ID:{((Job)job).Id}】【Num:{_num13}】");
            watchTime.Should().BeGreaterOrEqualTo(INTERVAL_13_14 - 2,$"【Job ID:{((Job)job).Id}】【Num:{_num13}】");
            _watch13.Reset();
            _watch13.Start();
            return true;
        }

        [Fact]
        public void TestForLogicalTimingOfJobIntervals1()
        {
            var flow = new JobManager();
            flow.Pool = new JobPool()
            {
                //关键属性：在组内整体轮循
                IsOverall = true
            };
            var job1 = new Job
            {
                Id = 1,
                IsLoop = true,
                LoopCount = LOOP_COUNT_13_14,
                Interval = INTERVAL_13_14,
                Timeout = INTERVAL_13_14*2,
                Run = CountFunc13
            };
            var job2 = new Job
            {
                Id = 2,
                IsLoop = true,
                LoopCount = LOOP_COUNT_13_14,
                Interval = INTERVAL_13_14,
                Timeout = INTERVAL_13_14 * 2,
                Run = CountFunc13
            };
            var job3 = new Job
            {
                Id = 3,
                IsLoop = true,
                LoopCount = LOOP_COUNT_13_14,
                Interval = INTERVAL_13_14,
                Timeout = INTERVAL_13_14 * 2,
                Run = CountFunc13
            };
            flow.Pool.AddRange(new IJobPoolItem[] { job1, job2, job3 });
            flow.Run();
        }

        [Fact]
        public void TestForLogicalTimingOfJobIntervals2()
        {
            var flow = new JobManager();
            flow.Pool = new JobPool()
            {
                //关键属性：单项工作结束后再进行下一项工作，不在组内整体轮循
                IsOverall = false
            };
            var job1 = new Job
            {
                Id = 1,
                IsLoop = true,
                LoopCount = LOOP_COUNT_13_14,
                Interval = INTERVAL_13_14,
                Timeout = INTERVAL_13_14 * 2,
                Run = CountFunc13
            };
            var job2 = new Job
            {
                Id = 2,
                IsLoop = true,
                LoopCount = LOOP_COUNT_13_14,
                Interval = INTERVAL_13_14,
                Timeout = INTERVAL_13_14 * 2,
                Run = CountFunc13
            };
            var job3 = new Job
            {
                Id = 3,
                IsLoop = true,
                LoopCount = LOOP_COUNT_13_14,
                Interval = INTERVAL_13_14,
                Timeout = INTERVAL_13_14 * 2,
                Run = CountFunc13
            };
            flow.Pool.AddRange(new IJobPoolItem[] { job1, job2, job3 });
            flow.Run();
        }

        #endregion

    }
}