using Mailer.Model;

namespace Mailer.Template
{
    public static class MailTemplate
    {


        public static string GetBody(User user)
        {
            string messageBody = $@"
                <html>
                <head>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            margin: 0;
                            padding: 20px;
                            background-color: #f4f4f4;
                        }}
                        .container {{
                            max-width: 600px;
                            margin: 0 auto;
                            background-color: #ffffff;
                            padding: 20px;
                            border-radius: 8px;
                            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                        }}
                        .header {{
                            background-color: #4CAF50;
                            color: #ffffff;
                            padding: 10px 0;
                            text-align: center;
                            border-radius: 8px 8px 0 0;
                        }}
                        .header h1 {{
                            margin: 0;
                            font-size: 24px;
                        }}
                        .content {{
                            padding: 20px;
                        }}
                        .content p {{
                            font-size: 16px;
                            margin: 10px 0;
                        }}
                        .footer {{
                            text-align: center;
                            color: #888888;
                            font-size: 14px;
                            padding: 10px 0;
                            border-top: 1px solid #dddddd;
                            margin-top: 20px;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h1>User Registration Details</h1>
                        </div>
                        <div class='content'>
                            <p><strong>Name:</strong> {user.Name}</p>
                            <p><strong>Email:</strong> {user.Email}</p>
                            <p><strong>Phone Number:</strong> {user.PhoneNumber}</p>
                            <p><strong>Location:</strong> {user.Location}</p>
                            <p><strong>User Type:</strong> {user.UserType}</p>
                        </div>
                        <div class='footer'>
                            <p>Thank you for registering with us!</p>
                            <p>If you have any questions, feel free to contact on this number +91-9899774341.</p>
                        </div>
                    </div>
                </body>
                </html>
                ";
            return messageBody;
        }

    }
}
