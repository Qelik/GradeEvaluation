# Copilot Instructions

## General Guidelines
- Ensure clarity and conciseness in all instructions.
- Use imperative mood for actionable items.

## Code Style
- Follow consistent formatting rules throughout the codebase.
- Adhere to established naming conventions for variables and functions.

## Project-Specific Rules
- Fix save method to avoid saving null or incorrect StudentId:
  - Ensure `Exam.StudentId` is set to the student's external id (`Student.StudentId`).
  - Match exams by `Exam.Id`.
  - Include related collections when querying.