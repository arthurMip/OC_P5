# Projet ASP.NET MVC avec .NET 8, EF Core et SQL Server

Ce projet est une application ASP.NET MVC utilisant .NET 8, Entity Framework Core et SQL Server. Ce fichier fournit des instructions pour installer et exécuter le projet en local.

## Prérequis

Avant de commencer, assurez-vous d'avoir installé les éléments suivants :

- [Visual Studio 2022](https://visualstudio.microsoft.com/fr/downloads/) (version 17.4 ou ultérieure recommandée)
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/fr-fr/sql-server) (ou SQL Server Express)
- [Git](https://git-scm.com/) (si vous clonez le projet depuis un dépôt Git)

## Installation

1. **Cloner le projet depuis GitHub** (si applicable) :

   ```bash
   git clone https://github.com/arthurMip/OC_P5.git
   ```

   Allez dans le répertoire du projet :

   ```bash
   cd OC_P5
   ```

2. **Ouvrir le projet dans Visual Studio** :

   - Lancez Visual Studio et sélectionnez **Fichier > Ouvrir > Projet/Solution**.
   - Sélectionnez le fichier `.sln` du projet pour l'ouvrir.

3. **Configurer la base de données** :

   - Ouvrez le fichier `appsettings.json` et configurez la chaîne de connexion à votre instance de SQL Server dans la section `ConnectionStrings`. Exemple :
     ```json
     {
       "ConnectionStrings": {
         "DefaultConnection": "Server=localhost;Database=NomDeVotreBase;Trusted_Connection=True;"
       }
     }
     ```

4. **Appliquer les migrations de la base de données** :
   - Ouvrez la **Console du gestionnaire de package** dans Visual Studio (Outils > Gestionnaire de package NuGet > Console du gestionnaire de package).
   - Exécutez la commande suivante pour créer la base de données et appliquer les migrations :
     ```bash
     Update-Database
     ```

## Exécution

1. **Lancer l'application** :

   - Appuyez sur `Ctrl + F5` ou cliquez sur le bouton **Démarrer sans débogage** dans Visual Studio pour lancer l'application.

2. **Accéder à l'application** :
   - Ouvrez votre navigateur web et allez à l'URL suivante :
     ```
     https://localhost:7091/
     ```
