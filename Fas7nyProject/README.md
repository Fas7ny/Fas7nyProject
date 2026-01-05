# clean_Architecture Referacnses
	-Presentation
	 ├── Application
	 │     └── Domain
	 └── Infrastructure
	       └── Domain  

# Structure Of clean_Architecture in this Project
  # Application
    # Cors(Query , Command)
	# Services
	# ServicesInterfaces
	#Dtos


  # Domain
    # Entities
    # RepoInterfaces
    # Enum
	
  # Infrastructure
      # Data (DbContextClass , Migrations , SeedData)
      # ExternalApi
	  # Repo

  # Presentaion
    # Controller

  # MainProject
    # Program.cs

  # Benefits of Clean Architecture:
     # - Maintainability: The separation of concerns makes it easier to modify and maintain the codebase
     #   without affecting other layers.
     # - Testability: Each layer can be tested independently, allowing for better unit testing and
     #   integration testing.
     # - Flexibility: The architecture allows for easy replacement of external dependencies without
     #   affecting the core business logic.


# Note : 
  # This is a simplified representation of Clean Architecture. In a real-world application, there
  # might be additional layers, components, and complexities based on specific requirements.
