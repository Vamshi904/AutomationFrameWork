@Regression
Feature: Location menu

Background:
	Given user is on login page
	When user logs in with valid credentials
	Then user should be logged in successfully
   
@AddLocationMenu @P1
Scenario: Add a new Location with valid details
	When the user navigates to the System tab
	And Click on the Location menu
	And click on the Add Location button
	And fills in the Location information with valid details
	And Click on the Add Location
	Then the new Location should appear in the user list

@DeleteLocation @P2
Scenario: Delete existing Location
	When the user navigates to the System tab
	And Click on the Location menu
	And Click on the delete icon in List of Locations page

@ExportExcelFunctionality @P3
Scenario: Verify Export Excel functionality in Location menu
	When the user navigates to the System tab
	And Click on the Location menu
	And selects a Location Name
	And the user clicks on the Export Excel functionality
	Then verify the Excel file is downloaded





