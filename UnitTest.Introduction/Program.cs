// Unit Testing : Unit in Unit Testing is the smallest part of a system that can be tested. Used to check whether a method works or not. And guarantees that our methods are working. Helps us find where and why errors occur in an app.
// Integration Testing : Testing parts of a system that interacts with other parts
// System Testing : Testing the whole system
// Acceptance Testing : Testing the whole system by real test users.

// Why do we need Unit Testing?

// We need to be sure that functionalities works as we intended
// Test Driven Design.
// Improves our code quality ( bug-fix loop, reusability, DI)
// Unit tests improves our refactoring speed as we identify the problematic functions.

// How to write Unit Tests

/*
        * The smallest parts are determined.
        * Create a testing scenario
        * Name the unit test
        * A Unit Test must be able to run independently.
        * Run quickly and produce results quickly
        * Error results must be understandable and must be instructive
 */

/*
    * Unit of Work
    * Unit Test
    * Assert
    * Arrange - Action - Assertion Blocks
    * Mock - Moq
    * FluentAssertion
    * Tools / Code Snippets Manager
 */

/*
    MockBehavior.Strict : Strict mode is used to ensure that the method is called with the expected parameters. If the method is not called with the expected parameters, the test will fail.
    MockBehavior.Loose : Loose mode is used to ensure that the method is called with the expected parameters. If the method is not called with the expected parameters, the parameters will be returned as default values and the test will not fail.
 */