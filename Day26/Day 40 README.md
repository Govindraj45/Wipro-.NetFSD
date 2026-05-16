# Problem Statement Day 40 - JWT Security API

`JwtSecurityApi` demonstrates:

- JWT login with signed tokens
- issuer, audience, lifetime, and signing-key validation
- role-based authorization with `Admin` and `User`
- HTTPS redirection for secure transport
- protected API endpoints requiring a valid `Authorization: Bearer <token>` header

Seeded users:
- `admin` / `Admin@123`
- `member` / `Member@123`
