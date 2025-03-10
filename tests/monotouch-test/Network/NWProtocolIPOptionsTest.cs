#if !__WATCHOS__
using System;
using System.Threading;
#if XAMCORE_2_0
using Foundation;
using Network;
using ObjCRuntime;
using CoreFoundation;
#else
using MonoTouch.Foundation;
using MonoTouch.Network;
using MonoTouch.CoreFoundation;
#endif

using NUnit.Framework;
using MonoTests.System.Net.Http;


namespace MonoTouchFixtures.Network {

	[TestFixture]
	[Preserve (AllMembers = true)]
	public class NWProtocolIPOptionsTest {

		AutoResetEvent connectedEvent;  // used to let us know when the connection was established so that we can access the NWPath
		string host;
		NWConnection connection;
		NWProtocolStack stack;
		NWProtocolIPOptions options; 

		void ConnectionStateHandler (NWConnectionState state, NWError error)
		{
			switch (state){
			case NWConnectionState.Ready:
				connectedEvent.Set ();
				break;
			case NWConnectionState.Invalid:
			case NWConnectionState.Failed:
				Assert.Inconclusive ("Network connection could not be performed.");
				break;
			}
		}

		[TestFixtureSetUp]
		public void Init ()
		{
			TestRuntime.AssertXcodeVersion (11, 0);
			// we want to use a single connection, since it is expensive
			connectedEvent = new AutoResetEvent(false);
			host = NetworkResources.MicrosoftUri.Host;
			using (var parameters = NWParameters.CreateTcp ())
			using (var endpoint = NWEndpoint.Create (host, "80")) {
				connection = new NWConnection (endpoint, parameters);
				connection.SetQueue (DispatchQueue.DefaultGlobalQueue); // important, else we will get blocked
				connection.SetStateChangeHandler (ConnectionStateHandler);
				connection.Start (); 
				Assert.True (connectedEvent.WaitOne (20000), "Connection timed out.");
				stack = parameters.ProtocolStack;
				using (var ipOptions = stack.InternetProtocol) {
					if (ipOptions != null) {
						ipOptions.IPSetVersion (NWIPVersion.Version4);
						stack.PrependApplicationProtocol (ipOptions);
					}
				}
			}
		}

		[TestFixtureTearDown]
		public void Dispose()
		{
			connection.Dispose ();
			stack.Dispose ();
		}

		[SetUp]
		public void SetUp ()
		{
			options = stack.InternetProtocol as NWProtocolIPOptions;
			Assert.NotNull (options, "options"); 
		}

		// we cannot assert that the C code those the right thing, BUT we do know
		// that if we call the property with the wrong pointer we will get an exception
		// from the runtime because the C lib does check the pointer that is used for the call

		[Test]
		public void SetIPVersionTest () => Assert.DoesNotThrow (() => options.SetVersion (NWIPVersion.Version6));

		[Test]
		public void SetHopLimitTest () => Assert.DoesNotThrow (() => options.SetHopLimit (1));


		[Test]
		public void SetUseMinimumMtu () => Assert.DoesNotThrow (() => options.SetUseMinimumMtu (true));


		[Test]
		public void SetDisableFragmentation () => Assert.DoesNotThrow (() => options.SetDisableFragmentation (true));


		[Test]
		public void SetCaculateReceiveTimeTest () => Assert.DoesNotThrow (() => options.SetCalculateReceiveTime (true));

		[Test]
		public void SetIPLocalAddressPreference () => Assert.DoesNotThrow (() => options.SetIPLocalAddressPreference (NWIPLocalAddressPreference.Temporary));
	}
}
#endif
