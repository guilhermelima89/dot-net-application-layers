## [N-LAYER]

# API

- AutoMapper
- Configurations
- Controllers
- Extensions
- Services (authentication)
- ViewModels
- Wwwroot (swagger dark mode)

# CORE

- Enumerators
- Interfaces
- Models
- Services
- Validations

# DATA

- Context
- Mappings
- Migrations
- Repositories

# TESTS

- Mapper
- Services

# Links

- https://learn.microsoft.com/pt-br/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures PT
- https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures EN
- https://macoratti.net/21/05/net_cleanarqrev1.htm PT

# EF Commands

- dotnet ef -s API migrations add 00 -p DATA
- dotnet ef -s API migrations remove -p DATA
- dotnet ef -s API database update -p DATA
