# SITConnect
SITConnect is a project dedicated to ensuring a secure and resilient user experience, focused on implementing robust security features to protect sensitive user data, prevent unauthorized access, and create a trustworthy online environment.

# Key Features
## Registration Form
- Implements strong password requirements with both client-side ASP.NET Validation Control and client-side Javascript password checker.
- Utilizes ASP.NET Validation Control for client and server-based password complexity checks.
## Securing User Data and Passwords
- Implements robust password protection using random salts and SHA256 hashing.
- Enhances data security by encrypting credit card information using the RijndaelManaged class.
## Session Management
- Establishes a secured session upon login to prevent session fixation.
- Implements session timeout with redirection to the homepage after 20 minutes of inactivity.
## Login/Logout
- Enables secure user login after registration.
- Implements account lockout after three consecutive login failures to mitigate brute-force attacks.
- Provides a clean logout process, destroying the user session.
## Anti-Bot Measures
- Integrates Google reCaptcha v3 service to prevent automated bot attacks.
## Proper Error Handling
- Implements graceful error handling on all pages, including custom 404 error pages.
## Advanced Features
- Allows account recovery after a specified lockout period.
- Prevents password reuse with a history of hashed passwords.
- Implements change password functionality with validation checks.
- Enforces minimum and maximum password age.
- Introduces two-factor authentication (2FA) through email verification.
