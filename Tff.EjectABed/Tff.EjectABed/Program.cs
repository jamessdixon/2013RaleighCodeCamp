using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Net.NetworkInformation;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;


namespace Tff.EjectABed
{
    public class Program
    {
        private static OutputPort _led = null;
        private static InterruptPort _button = null;
        private static Socket _serverSocket = null;
        private static PWM _servo = null;

        private const uint SERVO_UP = 1250;
        private const uint SERVO_DOWN = 1750;
        private const uint SERVO_NEUTRAL = 1500;

        private static bool _servoReady = true;

        public static void Main()
        {
            RunStartUpLight();
            SetUpButton();
            SetUpServo();
            SetUpWebServer();
            ListenForWebRequest();
        }

        private static void SetUpServo()
        {
            uint period = 20000;
            uint duration = SERVO_NEUTRAL;

            _servo = new PWM(PWMChannels.PWM_PIN_D5, period, duration, PWM.ScaleFactor.Microseconds, false);
            _servo.Start();
            _servoReady = true;
            Thread.Sleep(2000);
        }
        
        private static void SetUpButton()
        {
            _button = new InterruptPort(Pins.ONBOARD_BTN, true, Port.ResistorMode.PullDown, Port.InterruptMode.InterruptEdgeBoth);
            _button.OnInterrupt += _button_OnInterrupt;
        }

        static void _button_OnInterrupt(uint data1, uint data2, DateTime time)
        {
            if (data2 == 1)
            {
                _servo.Duration = SERVO_DOWN;
            }
            else
            {
                _servo.Duration = SERVO_NEUTRAL;
            }
        }

        private static void SetUpWebServer()
        {
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipEndpoint = new IPEndPoint(IPAddress.Any, 80);
            _serverSocket.Bind(ipEndpoint);
            Debug.Print(_serverSocket.
            ListenForWebRequest();
        }

        public static void ListenForWebRequest()
        {
            while (true)
            {
                using (Socket clientSocket = _serverSocket.Accept())
                {
                    if (_servoReady)
                    {
                        _servoReady = false;
                        String request = GetRequestFromSocket(clientSocket);
                        Thread thread = new Thread(() => HandleWebRequest(request));
                        thread.Start();
                        SendActivateResponse(clientSocket);
                    }
                    else
                    {
                        SendBusyResponse(clientSocket);
                    }
                }
            }
        }

        private static string GetRequestFromSocket(Socket clientSocket)
        {
            int bytesReceived = clientSocket.Available;
            if (bytesReceived > 0)
            {
                byte[] buffer = new Byte[bytesReceived];
                int byteCount = clientSocket.Receive(buffer, bytesReceived, SocketFlags.None);
                return new String(Encoding.UTF8.GetChars(buffer));
            }
            return String.Empty;
        }

        private static void HandleWebRequest(String request)
        {
            RequestValues requestValues = GetRequestValuesFromWebRequest(request);
            Thread.Sleep(requestValues.Duration);
            if (requestValues.Duration > 0)
            {
                ActivateServoForBellows(requestValues.Direction, requestValues.Duration);
            }
            _servoReady = true;
        }

        public static RequestValues GetRequestValuesFromWebRequest(String request)
        {
            RequestValues requestValues = new RequestValues();

            if (request.Length > 0)
            {
                String[] chunkedRequest = request.Split('/');
                for (int i = 0; i < chunkedRequest.Length; i++)
                {
                    chunkedRequest[i] = chunkedRequest[i].Trim();
                    chunkedRequest[i] = chunkedRequest[i].ToUpper();
                }
                requestValues.Verb = chunkedRequest[0];
                requestValues.Direction = chunkedRequest[1];

                Int32 duration = 0;
                if (chunkedRequest[2] == "HTTP")
                {
                    duration = 15000;
                }
                else
                {
                    try
                    {
                        duration = Int32.Parse(chunkedRequest[2]);
                    }
                    catch (Exception)
                    {
                        try
                        {
                            String[] chunkedDuration = chunkedRequest[2].Split(' ');
                            duration = Int32.Parse(chunkedDuration[0]);
                        }
                        catch (Exception)
                        {

                        }
                    }

                }
                requestValues.Duration = duration;
            }

            return requestValues;

        }

        private static void SendActivateResponse(Socket clientSocket)
        {
            String response = "The Eject-A-Bed activated at " + DateTime.Now.ToString();
            String header = @"HTTP/1.0 200 OK\r\nContent-Type: text;charset=utf-8\r\nContent-Length: " +
                response.Length.ToString() + "\r\nConnection: close\r\n\r\n";

            clientSocket.Send(Encoding.UTF8.GetBytes(header), header.Length, SocketFlags.None);
            clientSocket.Send(Encoding.UTF8.GetBytes(response), response.Length, SocketFlags.None);
        }

        private static void SendBusyResponse(Socket clientSocket)
        {
            String response = "The Eject-A-Bed is busy at " + DateTime.Now.ToString();
            String header = @"HTTP/1.0 503 Service Unavailable\r\nContent-Type: text;charset=utf-8\r\nContent-Length: " +
                response.Length.ToString() + "\r\nConnection: close\r\n\r\n";

            clientSocket.Send(Encoding.UTF8.GetBytes(header), header.Length, SocketFlags.None);
            clientSocket.Send(Encoding.UTF8.GetBytes(response), response.Length, SocketFlags.None);

        }

        private static void ActivateServoForBellows(String direction, Int32 duration)
        {
            if (direction == "UP")
            {
                _servo.Duration = SERVO_UP;
            }
            else if (direction == "DOWN")
            {
                _servo.Duration = SERVO_DOWN;
            }

            Thread.Sleep(duration);
            _servo.Duration = SERVO_NEUTRAL;
        }

        private static void RunStartUpLight()
        {
            _led = new OutputPort(Pins.ONBOARD_LED, false);
            BlinkY();
            BlinkY();
            BlinkZ();
            _led.Write(false);
        }

        private static void BlinkY()
        {
            BlinkDash();
            BlinkDot();
            BlinkDash();
            BlinkDash();
        }

        private static void BlinkZ()
        {
            BlinkDash();
            BlinkDash();
            BlinkDot();
            BlinkDot();
        }

        private static void BlinkDash()
        {
            _led.Write(true);
            Thread.Sleep(500);
            _led.Write(false);
            Thread.Sleep(250);
        }

        private static void BlinkDot()
        {
            _led.Write(true);
            Thread.Sleep(250);
            _led.Write(false);
            Thread.Sleep(250);
        }

        private static void ActivateServo()
        {
            uint period = 20;
            uint duration = 1;

            PWM servo = new PWM(PWMChannels.PWM_PIN_D5, period, duration, PWM.ScaleFactor.Milliseconds, false);
            servo.Start();

            Thread.Sleep(Timeout.Infinite);

        }

        private static void TestServoFullRange()
        {
            uint period = 20000;
            uint duration = 1500;

            PWM servo = new PWM(PWMChannels.PWM_PIN_D5, period, duration, PWM.ScaleFactor.Microseconds, false);
            servo.Start();

            Thread.Sleep(2000);
            servo.Duration = 2500;
            Thread.Sleep(3000);
            servo.Duration = 500;
            Thread.Sleep(3000);
            servo.Duration = 1500;
        }

    }
}
