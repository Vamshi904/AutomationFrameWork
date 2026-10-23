@Regression
Feature: Transition Menu

Background:
	Given user is on login page
	When user logs in with valid credentials
	Then user should be logged in successfully
   
Scenario Outline: Add a new Transition Point Information with valid details
	When the user navigates to the System tab
	And Click on the Transition Point menu
	And click on the Add new Transition Point button
	And fills in the Transition Point form with "<TransitionPointName>" "<UserOffice>" "<UserType>"  "<TransitionStatus>"
	And Click on the submit information Location
	Then the new Transition Point should appear in the user list

Examples:
	| TransitionPointName | UserOffice                      | UserType            | TransitionStatus                                                                 |
	| kamareddy01         | ADEN ADDE INTERNATIONAL AIRPORT | Transition Point    | Item collected                                                                   |
	| Delhi01             | Alfa Bright International       | Transition Point    | Further Delays by Airline. Cargo expected to arrive at Entebbe tomorrow.         |
	| Mumbai02            | ADEN ADDE INTERNATIONAL AIRPORT | Corresponding Agent | Delayed by airline. Scheduled to arrive at Entebbe Airport on Saturday, 20/12/25 |


Scenario Outline: Editing existing Transition Point with valid details
	When the user navigates to the System tab
	And Click on the Transition Point menu
	And the user clicks on the  Transition point Name  link
	And fills in the Transition Point form with "<TransitionPointName>" "<UserOffice>" "<UserType>"  "<TransitionStatus>"
	And Click on the submit information Location
	Then the Edit Transition Point should appear in the user list

Examples:
	| TransitionPointName | UserOffice                      | UserType            | TransitionStatus                                                                 |
	| Hyderabad01         | ADEN ADDE INTERNATIONAL AIRPORT | Transition Point    | Item collected                                                                   |
	| Delhi03             | Alfa Bright International       | Transition Point    | Further Delays by Airline. Cargo expected to arrive at Entebbe tomorrow.         |
	| Mumbai04            | ADEN ADDE INTERNATIONAL AIRPORT | Corresponding Agent | Delayed by airline. Scheduled to arrive at Entebbe Airport on Saturday, 20/12/25 |

@SearchFunctionalityinTransition
Scenario: Search Functionality and View, printing Transition Status table data
	When the user navigates to the System tab
	And Click on the Transition Point menu
	And Enter the name in the search Field in list of transition points
	Then print all the details in the Transition Point table

Scenario Outline: Search Functionality and View, printing Transition Status table data using the scenario outline
	When the user navigates to the System tab
	And Click on the Transition Point menu
	And Enter "<TransitionPointName>" in the search Field in list of transition points
	Then print all the details in the Transition Point table

Examples:
	| TransitionPointName |
	| Mumbai04            |
	| Mumbai02            |
	| Hyderabad01         |
	| Mumbai              |
	| Delhi               |

