# Eve Echoes Planetary Production API v0.5-alpha

## Current Build Status

![Build Status](https://dev.azure.com/zdravkovBG/Eve%20Echoes%20Industries/_apis/build/status/EveEchoesIndustiresApp%20-%20CI)

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
