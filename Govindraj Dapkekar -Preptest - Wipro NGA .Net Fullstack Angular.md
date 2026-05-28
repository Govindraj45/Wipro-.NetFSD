# ASSIGNMENT REPORT: SECURE LOGIN AND ROLE-BASED ACCESS IN ASP.NET CORE MVC

*   **Student Name:** Govindraj Dapkekar
*   **Assignment Title:** Preptest - Wipro NGA .Net Fullstack Angular
*   **Milestone:** Prep Test Milestone 3

---

## 1. Project Objective and Overview
This assignment demonstrates the implementation of robust security fundamentals, authentication, and role-based authorization in an ASP.NET Core MVC application. The main goal is to create a secure login portal that validates credentials, assigns roles, and restricts access to features based on those roles (Admin or User). Key modern security features such as CSRF tokens, forced HTTPS redirection, and the Data Protection APIs are integrated.

---

## 2. Security Fundamentals & Authentication

### 2.1 ASP.NET Core Identity Integration
The system integrates **ASP.NET Core Identity** alongside an **Entity Framework Core SQLite** database. The core classes utilized include:
*   `UserManager<IdentityUser>`: Used for creating users, checking user exists, retrieving user roles, and searching users by username.
*   `SignInManager<IdentityUser>`: Handles password validation, user sign-in/sign-out, and cookie token generation.
*   `AppDbContext`: Inherits from `IdentityDbContext<IdentityUser>` to automatically bootstrap ASP.NET Core Identity tables.

### 2.2 Password Security & Hashing
Passwords are never stored in plain text. When a user is created via the `UserManager`, the password is automatically hashed using the **PBKDF2 (Password-Based Key Derivation Function 2)** algorithm with HMAC-SHA256. This prevents credential exposure even if the database is compromised.

### 2.3 Automated Database Initialization & Seeding
To make the application fully portable and self-contained, a helper class `DbInitializer` runs on application startup:
1.  Invokes `context.Database.EnsureCreated()` to check if the database exists and automatically create it with correct table schemas if missing.
2.  Ensures `Admin` and `User` roles exist in the database.
3.  Seeds the following testing accounts:
    *   **Admin Account:** Username `admin` / Password `Admin@123` (Assigned to the `Admin` role).
    *   **User Account:** Username `user1` / Password `User@123` (Assigned to the `User` role).

---

## 3. Role-Based Authorization and Access Control

### 3.1 Controller Routing & Role Attributes
Access to controller actions is restricted using security attributes:
*   **Admin Dashboard:** The `AdminController` is decorated with `[Authorize(Roles = "Admin")]`. Any user without this role trying to access the controller action `/Admin/Dashboard` is blocked.
*   **User Profile:** The profile action on `AccountController` is decorated with `[Authorize(Roles = "User")]`, ensuring only authenticated standard users can see it.

### 3.2 Login Redirection Flow
Upon successful authentication in `AccountController.cs` (using `SignInManager.PasswordSignInAsync`), the system determines the user's role:
*   Users in the **Admin** role are redirected to `/Admin/Dashboard` with a success message.
*   Users in the **User** role are redirected to `/Account/UserProfile` with their profile card.
*   Unauthenticated requests to secure URLs are intercepted by the authentication middleware and redirected to the login page (`/Account/Login`).

---

## 4. Securing MVC Applications

### 4.1 Cross-Site Request Forgery (CSRF) Mitigation
To protect against CSRF attacks, the application uses **AntiForgeryTokens**:
*   All state-changing forms (such as Login and Logout) include the hidden token in their HTML markup.
*   The corresponding POST action handlers are decorated with the `[ValidateAntiForgeryToken]` attribute. This ensures the form token matches the user's session cookie token, preventing malicious third-party requests.

### 4.2 Transport Layer Security (Global HTTPS)
To force the application to use HTTPS for all requests:
*   `app.UseHttpsRedirection()` is registered in the request middleware pipeline.
*   A global `RequireHttpsAttribute` filter is added during service configuration:
    ```csharp
    builder.Services.AddControllersWithViews(options =>
    {
        options.Filters.Add(new RequireHttpsAttribute());
    });
    ```
This guarantees that all client-server communications are encrypted in transit.

### 4.3 Data Protection APIs
To demonstrate the protection of sensitive information, the **ASP.NET Core Data Protection API** (`IDataProtectionProvider`) is implemented. On the User Profile view, sensitive contact data (address and telephone details) is protected in-memory using an encrypted key ring and only decrypted when an authorized user accesses their profile.

---

## 5. Verification Results and Screenshots

The application was run locally and verified against all required test matrices. Below are the details and screenshot references to attach to your submission.

### 5.1 Test Matrix

| Step | Test Case | Input Details | Expected Outcome | Screenshot Reference |
| :--- | :--- | :--- | :--- | :--- |
| **1** | Navigate to Home | Open `https://localhost:7245` | Shows clean landing page detailing security features and a Login button. | `1_Homepage.png` |
| **2** | Login Page | Navigate to `/Account/Login` | Displays secure login form and a testing credentials info box. | `2_Login_Page.png` |
| **3** | Admin Login | User: `admin` <br> Pass: `Admin@123` | Redirects to `/Admin/Dashboard` with message: *"Welcome, Admin! You have access to the Admin Dashboard."* and a table displaying user details. | `3_Admin_Dashboard.png` |
| **4** | User Profile | User: `user1` <br> Pass: `User@123` | Redirects to `/Account/UserProfile` with message: *"Welcome, user1! Here is your profile information."* Shows dynamic decryption. | `4_User_Profile.png` |
| **5** | Access Denied | Log in as `user1` and navigate to `/Admin/Dashboard` | Blocks access and redirects to `/Account/AccessDenied` with message: *"Access Denied: You do not have permission to view this page."* | `5_Access_Denied.png` |

### 5.2 Visual Screenshots Evidence

#### 1. Secure Landing Homepage
Shows the clean landing page explaining the implemented security features (Identity, HTTPS, Anti-Forgery, Data Protection APIs) and offering dynamic sign-in links:

![Secure Homepage](Govindraj%20Dapkekar%20-Preptest%20-%20Wipro%20NGA%20.Net%20Fullstack%20Angular/Screenshots/1_Homepage.png)

#### 2. Secure Login Portal
Displays the authentication credentials form complete with testing hints for the evaluator:

![Secure Login Portal](Govindraj%20Dapkekar%20-Preptest%20-%20Wipro%20NGA%20.Net%20Fullstack%20Angular/Screenshots/2_Login_Page.png)

#### 3. Administrative Control Dashboard (Admin Access)
Displays the dashboard populated with details of the registered user accounts, proving role-based data retrieval:

![Admin Control Dashboard](Govindraj%20Dapkekar%20-Preptest%20-%20Wipro%20NGA%20.Net%20Fullstack%20Angular/Screenshots/3_Admin_Dashboard.png)

#### 4. Secure User Profile and Data Protection Decryption Demo (User Access)
Displays the user profile card with the dynamic Data Protection API demonstrating payload encryption and real-time decryption:

![User Profile Card](Govindraj%20Dapkekar%20-Preptest%20-%20Wipro%20NGA%20.Net%20Fullstack%20Angular/Screenshots/4_User_Profile.png)

#### 5. Role-Based Access Control Blocked Access (Access Denied)
Shows the warning message displayed when a user with the `User` role attempts to bypass navigation restrictions and manually access `/Admin/Dashboard`:

![Access Denied Page](Govindraj%20Dapkekar%20-Preptest%20-%20Wipro%20NGA%20.Net%20Fullstack%20Angular/Screenshots/5_Access_Denied.png)

---

## 6. Project Source Code Snippets

### 6.1 Service Configuration & Middlewares (`Program.cs`)
```csharp
// Configure SQLite Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

// Configure ASP.NET Core Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Configure Application Cookie paths
builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

// Register Data Protection APIs & Require HTTPS Globally
builder.Services.AddDataProtection();
builder.Services.AddControllersWithViews(options => {
    options.Filters.Add(new RequireHttpsAttribute());
});
```

### 6.2 Role Authorization Enforcement (`AdminController.cs`)
```csharp
[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    // Constructor injection and Dashboard Action...
}
```

### 6.3 Data Protection Integration (`AccountController.cs`)
```csharp
[Authorize(Roles = "User")]
public IActionResult UserProfile()
{
    var username = User.Identity?.Name ?? "User";
    string sensitiveData = "Phone: +1 (555) 019-2831 | Address: 123 Secure Way, Redmond, WA";
    string encryptedData = _protector.Protect(sensitiveData);
    string decryptedData = _protector.Unprotect(encryptedData);

    ViewBag.Message = $"Welcome, {username}! Here is your profile information.";
    ViewBag.EncryptedSensitiveData = encryptedData;
    ViewBag.DecryptedSensitiveData = decryptedData;

    return View();
}
```
