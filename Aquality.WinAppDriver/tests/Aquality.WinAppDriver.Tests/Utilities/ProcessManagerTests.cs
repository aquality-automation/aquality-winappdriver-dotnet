using Aquality.WinAppDriver.Applications;
using Aquality.WinAppDriver.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Aquality.WinAppDriver.Tests.Utilities
{
    public class ProcessManagerTests
    {
        private const string TestProcess = "cmd.exe";
        private static IProcessManager ProcessManager => AqualityServices.ProcessManager;

        private static IEnumerable<Func<string, bool>> FunctionsReturnTrueWhenProcessStarted
        {
            get
            {
                yield return (name) => ProcessManager.TryToStopProcesses(GetPureName(name));
                yield return (name) => ProcessManager.IsProcessRunning(GetPureName(name));
                yield return (name) => ProcessManager.TryToStopExecutables(name);
                yield return (name) => ProcessManager.IsExecutableRunning(name);
            }
        }

        private static IEnumerable<Action<string>> FunctionsThatStopExecutable
        {
            get
            {
                yield return (name) => ProcessManager.TryToStopProcesses(GetPureName(name));
                yield return (name) => ProcessManager.TryToStopExecutables(name);
            }
        }

        private static string GetPureName(string name) => name.Replace(".exe", string.Empty);

        [Test]
        public void Should_BePossibleTo_StartProcess()
        {
            using var process = ProcessManager.Start(TestProcess);
            Assert.That(process, Is.Not.Null);
        }

        [TestCaseSource(nameof(FunctionsThatStopExecutable))]
        public void Should_BePossibleTo_StopExecutable_WhenItIsRunning(Action<string> action)
        {
            using var process = Process.Start(TestProcess);
            action(TestProcess);
            Assert.That(process.HasExited, Is.True);
        }

        [TestCaseSource(nameof(FunctionsReturnTrueWhenProcessStarted))]
        public void Should_BePossibleTo_WorkWithProcessManager_WhenProcessIsNotRunning(Func<string, bool> func)
        {
            using var process = Process.Start(TestProcess);
            Assert.That(func(TestProcess + "fake"), Is.False);
        }

        [TestCaseSource(nameof(FunctionsReturnTrueWhenProcessStarted))]
        public void Should_BePossibleTo_WorkWithProcessManager_WhenProcessIsRunning(Func<string, bool> func)
        {
            using var process = Process.Start(TestProcess);
            Assert.That(func(TestProcess), Is.True);
        }
    }
}
