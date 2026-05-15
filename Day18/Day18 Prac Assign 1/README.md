# Day 18 Practice Assignment 1 - Online Book Store REST API

ASP.NET Core REST API for managing books and authors with in-memory storage.

## Routes

| Method | Route | Description |
| --- | --- | --- |
| GET | `/api/books` | List all books |
| GET | `/api/books/{id}` | Get one book |
| POST | `/api/books` | Create a book |
| PUT | `/api/books/{id}` | Update a book |
| DELETE | `/api/books/{id}` | Delete a book |
| GET | `/api/authors` | List all authors |
| GET | `/api/authors/{id}` | Get one author |
| GET | `/api/authors/{authorId}/books` | List books for an author |
| POST | `/api/authors` | Create an author |
| PUT | `/api/authors/{id}` | Update an author |
| DELETE | `/api/authors/{id}` | Delete an author |

## Sample Book JSON

```json
{
  "title": "Emma",
  "authorId": 2,
  "publicationYear": 1815
}
```

The API returns standard REST status codes such as `200 OK`, `201 Created`, `204 No Content`, `400 Bad Request`, and `404 Not Found`.
