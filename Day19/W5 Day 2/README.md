# W5 Day 2 - Full ASP.NET Core Online Bookstore

`OnlineBookstoreApp` mixes Razor Pages and MVC in one project:

- Razor Pages: registration, login, cart management, and admin book inventory management.
- MVC: book catalog, book details, order summary, and order confirmation.
- Custom validation: ISBN and price rules on `Book`.
- Filters: global exception handling, request logging, session cart initialization, and admin inventory access control.
- Session management: cart items persist in session.
- Routing: custom `catalog` and `catalog/{id}` routes.
- Design patterns: repository abstraction for book access and service classes for users, cart, and orders.

Admin login:
- Username: `admin`
- Password: `Admin@123`
