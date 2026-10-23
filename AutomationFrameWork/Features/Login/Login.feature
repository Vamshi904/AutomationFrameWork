@Login
Feature: Login

@UserLogin
Scenario: Successful login with valid credentials
	Given user is on login page
	When user logs in with valid credentials
	Then user should be logged in successfully
	And user must logout on clicking the logout button