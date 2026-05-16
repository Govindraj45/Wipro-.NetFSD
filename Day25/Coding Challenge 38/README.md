# Coding Challenge 38 - Secure Shopping Platform

`SecureShoppingPlatform` is an ASP.NET Core MVC app with:

- server-side validation for username, email, and strong passwords
- cookie authentication with `Admin` and `Customer` roles
- output-encoded product reviews rendered through Razor
- anti-forgery protection on registration, login, logout, and review submission
- password hashing with ASP.NET Core `PasswordHasher`
- a simple login lockout after repeated failures to reduce brute-force attempts

Admin login:
- Username: `admin`
- Password: `Admin@123`
