using System;
using NKnife.Win.UpdaterFromGitHub.Base;

namespace NKnife.Win.UpdaterFromGitHub.App
{
    class UpdateStatusChangedEventArgs : EventArgs
    {
        public UpdateStatus Status { get; private set; }
        public string Message { get; private set; }

        public UpdateStatusChangedEventArgs(UpdateStatus status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}