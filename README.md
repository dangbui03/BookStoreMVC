## Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)

### Building the Project

To build the project, run the following command:

```sh
dotnet build
```

### Running the Project

To run the project, use the following command:

```sh
dotnet run
```

### Running Tests

To run the tests, use the following command:

```sh
dotnet test
```

### Docker Support

To build and run the project using Docker, use the following commands:

```sh
docker build -t bookstoremvc .
docker run -d -p 8080:80 bookstoremvc
```

### Contributing

If you would like to contribute, please fork the repository and use a feature branch. Pull requests are warmly welcome.

### License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.