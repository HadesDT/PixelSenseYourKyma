/**
 *    Copyright 2013 Eric Schayes
 *
 *  Licensed under the Apache License, Version 2.0 (the "License 
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *  
 *      http://www.apache.org/licenses/LICENSE-2.0
 *      
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;

namespace PixelsenseYourKyma
{
    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    public partial class SurfaceWindow1 : SurfaceWindow
    {

        Double Position;
        String ip = "172.30.8.16"; //Here is the ip of the computer where the kyma is connected.
        int port = 8000; //Here is the port of the computer where the kyma is connected.

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SurfaceWindow1()
        {
            InitializeComponent();

            // Add handlers for window availability events
            AddWindowAvailabilityHandlers();
        }

        /// <summary>
        /// Occurs when the window is about to close. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Remove handlers for window availability events
            RemoveWindowAvailabilityHandlers();
        }

        /// <summary>
        /// Adds handlers for window availability events.
        /// </summary>
        private void AddWindowAvailabilityHandlers()
        {
            // Subscribe to surface window availability events
            ApplicationServices.WindowInteractive += OnWindowInteractive;
            ApplicationServices.WindowNoninteractive += OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable += OnWindowUnavailable;
        }

        /// <summary>
        /// Removes handlers for window availability events.
        /// </summary>
        private void RemoveWindowAvailabilityHandlers()
        {
            // Unsubscribe from surface window availability events
            ApplicationServices.WindowInteractive -= OnWindowInteractive;
            ApplicationServices.WindowNoninteractive -= OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable -= OnWindowUnavailable;
        }

        /// <summary>
        /// This is called when the user can interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowInteractive(object sender, EventArgs e)
        {
            //TODO: enable audio, animations here
        }

        /// <summary>
        /// This is called when the user can see but not interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowNoninteractive(object sender, EventArgs e)
        {
            //TODO: Disable audio here if it is enabled

            //TODO: optionally enable animations here
        }

        /// <summary>
        /// This is called when the application's window is not visible or interactive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowUnavailable(object sender, EventArgs e)
        {
            //TODO: disable audio, animations here
        }

        private void OnTouchDown(object sender, TouchEventArgs args)
        {
            //Check if tag
            bool isTag = args.TouchDevice.GetIsTagRecognized();

            if (isTag)
            {
                var tqg = args.TouchDevice.GetTagData();
                Console.WriteLine(tqg.Value);
                OSC oscm = new OSC(ip, port);
                UIElement relativeTo = this;
                OSCmsg omess = new OSCmsg("/vcs");
                OSCmsg omess2 = new OSCmsg("/vcs");
                //We check what is the tag value, and send the right parameter via OSC message
                switch (tqg.Value)
                {
                    case 129:
                        omess.addValue(3145731);
                        break;
                    case 128:
                        omess.addValue(3145732);
                        break;
                    case 131:
                        omess.addValue(3145733);
                        break;
                    case 132:
                        omess.addValue(3145734);
                        break;
                    case 134:
                        omess.addValue(3145729);
                        break;
                    default:
                        omess.addValue(3145735);
                        omess2.addValue(2);
                        omess2.addValue(0.4f);
                        oscm.sendOSCmsg(omess2);
                        break;
                }
                omess.addValue((float)(args.TouchDevice.GetOrientation(relativeTo) / 360));
                oscm.sendOSCmsg(omess);
            }
            args.TouchDevice.Capture(ActiveArea);
        }

        private void OnTouchUp(object sender, TouchEventArgs args)
        {
            //Check if tag
            bool isTag = args.TouchDevice.GetIsTagRecognized();

            //When we take the object, we put all the value to zero
            if (isTag)
            {
                OSC oscm = new OSC(ip, port);
                UIElement relativeTo = this;
                Position = args.TouchDevice.GetOrientation(relativeTo);
                var tqg = args.TouchDevice.GetTagData();
                OSCmsg omess = new OSCmsg("/vcs");
                OSCmsg omess2 = new OSCmsg("/vcs");
                switch (tqg.Value)
                {
                    case 129:
                        omess.addValue(3145731);
                        omess.addValue(0.0f);
                        break;
                    case 128:
                        omess.addValue(3145732);
                        omess.addValue(0.0f);
                        break;
                    case 131:
                        omess.addValue(3145733);
                        omess.addValue(0.0f);
                        break;
                    case 132:
                        omess.addValue(3145734);
                        omess.addValue(0.0f);
                        break;
                    case 134:
                        omess.addValue(3145729);
                        omess.addValue(1.0f);
                        break;
                    default:
                        omess.addValue(3145735);
                        omess2.addValue(2);
                        omess2.addValue(0.0f);
                        oscm.sendOSCmsg(omess2);
                        omess.addValue(0.0f);
                        break;
                }
                oscm.sendOSCmsg(omess);
            }

            args.TouchDevice.Capture(ActiveArea);
        }

        private void OnTouchMove(object sender, TouchEventArgs args)
        {
            //Check if tag
            bool isTag = args.TouchDevice.GetIsTagRecognized();

            if (isTag)
            {
                var tqg = args.TouchDevice.GetTagData();
                Console.WriteLine(tqg.Value);
                OSC oscm = new OSC(ip, port);
                UIElement relativeTo = this;
                OSCmsg omess = new OSCmsg("/vcs");
                OSCmsg omess2 = new OSCmsg("/vcs");
                //We check what is the tag value, and send the right parameter via OSC message
                switch (tqg.Value)
                {
                    case 129:
                        omess.addValue(3145731);
                        break;
                    case 128:
                        omess.addValue(3145732);
                        break;
                    case 131:
                        omess.addValue(3145733);
                        break;
                    case 132:
                        omess.addValue(3145734);
                        break;
                    case 134:
                        omess.addValue(3145729);
                        break;
                    default:
                        omess.addValue(3145735);
                        omess2.addValue(2);
                        omess2.addValue(0.4f);
                        oscm.sendOSCmsg(omess2);
                        break;
                }
                omess.addValue((float)(args.TouchDevice.GetOrientation(relativeTo) / 360));
                oscm.sendOSCmsg(omess);
            }
            args.TouchDevice.Capture(ActiveArea);
        }

        private void surfaceButton_Exit(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(1);
        }
    }
}