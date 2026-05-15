# Day 18 Practice Assignment 2 - Online Movie Catalog REST API

ASP.NET Core REST API for managing movies and directors with in-memory storage.

## Routes

| Method | Route | Description |
| --- | --- | --- |
| GET | `/api/movies` | List all movies |
| GET | `/api/movies/{id}` | Get one movie |
| POST | `/api/movies` | Create a movie |
| PUT | `/api/movies/{id}` | Update a movie |
| DELETE | `/api/movies/{id}` | Delete a movie |
| GET | `/api/directors` | List all directors |
| GET | `/api/directors/{id}` | Get one director |
| GET | `/api/directors/{directorId}/movies` | List movies for a director |
| POST | `/api/directors` | Create a director |
| PUT | `/api/directors/{id}` | Update a director |
| DELETE | `/api/directors/{id}` | Delete a director |

## Sample Movie JSON

```json
{
  "title": "Barbie",
  "directorId": 2,
  "releaseYear": 2023
}
```

The API returns standard REST status codes such as `200 OK`, `201 Created`, `204 No Content`, `400 Bad Request`, and `404 Not Found`.
