﻿using Cake.Core;
using Cake.Talend.Tests.Fixture;
using Cake.Testing;
using Should;
using System;
using Xunit;

namespace Cake.Talend.Tests
{
    public sealed class TalendCommandLineRunnerTests {
        [Fact]
        public void Should_Throw_If_Settings_Are_Null() {
            // Given
            var fixture = new TalendCommandLineRunnerFixture();
            fixture.Settings = null;

            // When
            var result = Record.Exception(() => fixture.Run());

            // Then
            result.ShouldBeType<ArgumentNullException>().ParamName.ShouldEqual("settings");
        }

        [Fact]
        public void Should_Throw_If_ProjectName_Is_Null() {
            // Given
            var fixture = new TalendCommandLineRunnerFixture();
            fixture.ProjectName = null;

            // When
            var result = Record.Exception(() => fixture.Run());

            // Then
            result.ShouldBeType<ArgumentNullException>().ParamName.ShouldEqual("projectName");
        }

        [Fact]
        public void Should_Throw_If_JobName_Is_Null() {
            // Given
            var fixture = new TalendCommandLineRunnerFixture();
            fixture.JobName = null;

            // When
            var result = Record.Exception(() => fixture.Run());

            // Then
            result.ShouldBeType<ArgumentNullException>().ParamName.ShouldEqual("jobName");
        }

        [Fact]
        public void Should_Throw_If_ArtifactDestination_Is_Null() {
            // Given
            var fixture = new TalendCommandLineRunnerFixture();
            fixture.ArtifactDestination = null;

            // When
            var result = Record.Exception(() => fixture.Run());

            // Then
            result.ShouldBeType<ArgumentNullException>().ParamName.ShouldEqual("path");
        }

        [Fact]
        public void Should_Throw_If_Process_Has_A_Non_Zero_Exit_Code() {
            // Given
            var fixture = new TalendCommandLineRunnerFixture();
            fixture.GivenProcessExitsWithCode(1);

            // When
            var result = Record.Exception(() => fixture.Run());

            // Then
            result.ShouldBeType<CakeException>()
                .Message.ShouldEqual("Talend Command Line: Process returned an error (exit code 1).");
        }

        [Fact]
        public void Should_Throw_If_Process_Was_Not_Started() {
            // Given
            var fixture = new TalendCommandLineRunnerFixture();
            fixture.GivenProcessCannotStart();

            // When
            var result = Record.Exception(() => fixture.Run());

            // Then
            result.ShouldBeType<CakeException>().Message.ShouldEqual("Talend Command Line: Process was not started.");
        }

        [Fact]
        public void Should_Start_Arguments_With_CommandLine_Options() {
            // Given 
            var fixture = new TalendCommandLineRunnerFixture();

            // When
            var result = fixture.Run();

            // Then
            result.Args.ShouldStartWith("-nosplash -application org.talend.commandline.CommandLine -consoleLog -data .");
        }
    }
}
