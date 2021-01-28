# Eve Echoes Planetary Production API 0.7-alpha

Whats new in version 0.7:
- Blueprints 
- Search for Blueprints
- Blueprint Calculator
- UI Fixes
- Bug Fixes

[![Build Status](https://dev.azure.com/zdravkovBG/Eve%20Echoes%20Industries/_apis/build/status/pirocorp.Eve-Echoes-Planetary-Production-API?branchName=main)](https://dev.azure.com/zdravkovBG/Eve%20Echoes%20Industries/_build/latest?definitionId=4&branchName=main)

Whats new in version 0.6:
- Best system in range bug fix. When you click on the row with the system subtable wont open.
- Navigation bugs (a lot of them) fix. 
- Improved loading times
- Add loading animations in pagination.

This projects aims to help players in Eve Echoes with industries in the game.

[Echoes Industries](https://www.echoesindustries.com/) Every time page is open random system is selected.

Currently supported features
- Navigation
  - [Exploring Regions](https://www.echoesindustries.com/navigation/regions).
  - [Exploring Cconstellations](https://www.echoesindustries.com/navigation/constellations).
  - [Exploring Systems](https://www.echoesindustries.com/navigation/systems).
- Resources
  - [Current Prices](https://www.echoesindustries.com/resources/details) Planetary produced resources prices and when is the last time they were updated.
  - [Best Resources In System](https://www.echoesindustries.com/resources/system) Returns best resources in current system based on given price.
  - [Best Resources In Constellation](https://www.echoesindustries.com/resources/constellation) Returns best resources in current constellation based on given price.
  - [Best Resources In Region](https://www.echoesindustries.com/resources/region) Returns best resources in current region based on given price.
  - [Best Resources In Range](https://www.echoesindustries.com/resources/range) Returns best resources in range of given number of jumps and given price.
- Systems
  - [System Details](https://www.echoesindustries.com/systems) Returns all resources produced in given system and their output and price. Can be sorted by price and output.
  - [Best System In Constellation](https://www.echoesindustries.com/systems/constellation) Calculates system with most valuable resource based on number of colonies and given prices. Can show which resource from which planet to produce in each colony.
  - [Best System in Region](https://www.echoesindustries.com/systems/region) Calculates system with most valuable resources based on number of colonies and given prices. Can show which resource from which planet in the system to produce in each colony to get calculated value.
  - [Best System in Range](https://www.echoesindustries.com/systems/range) Calculates system with most valuable resources based on number of colonies, given prices and given range (jumps). Can show which resource from which planet in the system to produce in each colony to get calculated value.
- [Search](https://www.echoesindustries.com/) Search for given system. When system is selected it became your location.

## Demo
![Demo](Demo.gif)

## Manuals

[How To Configure CI For Blazor WASM Hosted Application](./manuals/How%20To%20Configure%20CI%20For%20Blazor%20WASM%20Hosted%20Application.md)

[How Configure CD To Deploy Blazor WASM Hosted Application](./manuals/How%20Configure%20CD%20To%20Deploy%20Blazor%20WASM%20Hosted%20Application.md)

[Add SSL to Azure Web App using LetsEncrypt](./manuals/Add%20SSL%20to%20Azure%20Web%20App%20using%20LetsEncrypt.md)
