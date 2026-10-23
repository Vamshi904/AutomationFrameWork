@Regression
Feature: Tracker Users menu

Background:
	Given user is on login page
	When user logs in with valid credentials
	Then user should be logged in successfully
   
@AddTrackerUser @P1
Scenario: Add a new tracker user  with valid details
	When the user navigates to the System tab
	And Click on the Tracker User menu
	And the user clicks on the Add Tracker User button
	And fills in the Track User Form with valid details
	And Click on the Submit information button
	Then Verify the confirmation message is displayed

@EditTrackerUser @P2
Scenario: Editing  tracker user  with valid details
	When the user navigates to the System tab
	And Click on the Tracker User menu
	And Click on the Name link
	And Editing the existing the details
	And Click on the Submit information button
	Then Verify the Update confirmation message is displayed

	@SearchFunctionality 
Scenario: Search and view   tracker user with valid details
	When the user navigates to the System tab
	And Click on the Tracker User menu
	And Fill the search Name in tracker user  
	Then print all the details in the tracker user table

