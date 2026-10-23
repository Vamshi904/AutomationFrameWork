@Regression
Feature: Add2Cart

Cart @add2Cart @P1
@ignore
Scenario: Validate Add2Cart Functionality
    Given user is on login page
    When user logs in with valid credentials
    Then user should be logged in successfully
    And validate add to cart functionality
    Then user must logout on clicking the logout button