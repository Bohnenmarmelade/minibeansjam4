using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Object = System.Object;

namespace Tests
{
    public class SchedulerTest
    {
        [Test]
        public void shouldIncreaseElapsedTime_whenUpdated()
        {
            // given
            float timeElapsed = 1.337f;
            Scheduler scheduler = new Scheduler(float.MaxValue, () => {});

            // when
            scheduler.Update(timeElapsed);

            // then
            Assert.AreEqual(timeElapsed, scheduler.TotalTimeElapased);
        }

        [Test]
        public void shouldAddElapsedTime_whenUpdated()
        {
            // given
            float timeElapsed = 1.337f;
            Scheduler scheduler = new Scheduler(float.MaxValue, () => {});
            scheduler.Update(timeElapsed);

            // when
            scheduler.Update(timeElapsed);

            // then
            Assert.AreEqual(timeElapsed * 2, scheduler.TotalTimeElapased);
        }

        [Test]
        public void shouldFireCallback_whenTimeIsReached()
        {
            // given
            bool isCalled = false;
            Action spyFunc = (() => { isCalled = true; });
            
            float timeToFire = 1.337f;
            Scheduler scheduler = new Scheduler(timeToFire, spyFunc);

            // when
            scheduler.Update(timeToFire + 0.001f);

            // then
            Assert.IsTrue(isCalled);
        }
        
        [Test]
        public void shouldFireCallbackRepeatedly_whenTimeIsReachedRepeatedly_givenRepeatIsTrue()
        {
            // given
            int calledNTimes = 0;
            Action spyFunc = (() => { calledNTimes++; });
            
            float timeToFire = 1.337f;
            Scheduler scheduler = new Scheduler(timeToFire, spyFunc, true);

            // when
            scheduler.Update(timeToFire + 0.001f);
            scheduler.Update(timeToFire + 0.001f);

            // then
            Assert.AreEqual(2, calledNTimes);
        }
        
        [Test]
        public void shouldNotFireCallbackRepeatedly_whenTimeIsReachedRepeatedly_givenRepeatIsFalse()
        {
            // given
            int calledNTimes = 0;
            Action spyFunc = () => { calledNTimes++; };
            
            float timeToFire = 1.337f;
            Scheduler scheduler = new Scheduler(timeToFire, spyFunc, false);

            // when
            scheduler.Update(timeToFire + 0.001f);
            scheduler.Update(timeToFire + 0.001f);

            // then
            Assert.AreEqual(1, calledNTimes);
        }
        
        
    }
}