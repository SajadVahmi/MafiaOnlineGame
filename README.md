# Implementation of Online Mafia Game with Microservices Architecture

## Overview
The project is a customized implementation of the Original Mafia Game, a social deduction game where players take on roles of either Mafia members or Honest citizens. Drawing inspiration from the Original Mafia Game, our version introduces modifications to enhance the online multiplayer experience.

## Bounded Contexts

1. **Identity Provider Context:** Manages authentication and identity information. We have implemented this context using [Duende Identity](https://duendesoftware.com/products/identityserver) and integrated it with Asp.net Identity for seamless user authentication and authorization. Currently under active development, implementing features for user management and access control. Additionally, API development is in progress for the admin panel.

2. **Games Context:** Handles the logic and data related to games.

3. **Players Context:** Manages player profiles. Utilizing Domain Driven Design Tactical patterns with three levels of testing have been implemented. Currently under active development, focusing on player profiles.

4. **Scoring Context:** Deals with scoring and points in the system.

5. **Framework:** Encompasses standards and auxiliary tools for the development process. It is an evolving set of guidelines and tools that grows and matures throughout the project development.

Each Bounded Context encapsulates a specific domain and defines its own ubiquitous language and model within the microservices architecture.

## Mafia Game Resources
- [How to Play Mafia](https://www.thespruce.com/how-to-play-mafia-411017): A step-by-step guide on how to play the Original Mafia Game.
- [Official Mafia Game Rules](https://www.servinglibrary.org/journal/2/the-original-mafia-rules): The official rules of the Original Mafia Game.

## Contributing
If you want to contribute to the project and help in its development, you can reach out to me on [LinkedIn](https://www.linkedin.com/in/sajad-vahmi/).
