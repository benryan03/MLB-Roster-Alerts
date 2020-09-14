MLB-Roster-Alerts will send you an email from a Gmail account that you own, with your favorite MLB team's roster moves from the previous day.

Recommended usage is to configure it as a Scheduled Task, to trigger each day at midnight.

MLB-Roster-Alerts.exe requires 4 arguments - your preferred team, the recipient's email address, the sender's Gmail address, and the sender's Gmail password.

Example:
MLB-Roster-Alerts.exe redsox recipient@domain.com sender@gmail.com password123

Setup instructions:

1. Copy \bin\Release\netcoreapp3.1 folder to your PC
2. Open Task Scheduler and click Create Basic Task
3. Task should trigger daily at 12:00 AM
4. For Action, select MLB-Roster-Alerts.exe
5. Add arguments as specified above.
6. Make sure "Allow less secure apps" is turned on in the settings for the sending Gmail account.
