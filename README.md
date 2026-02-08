# CWN Student Math Test System

## Overview
This project implements a student math test evaluation system as part of a technical interview assignment.

The system allows teachers to upload XML files containing student exams, automatically evaluates arithmetic tasks, persists results, and provides analytical views for students.

## Architecture
- ASP.NET MVC (.NET Framework 4.8)
- Entity Framework 6
- SQL Server
- Independent Math Processor
- Independent Web API for third-party integrations

## Features
- Batch XML exam processing
- Automatic grading of arithmetic expressions
- Teacher upload UI
- Student analytics view
- REST API integration point
- Unit test coverage

## XML Input Example
```xml
<Teacher ID="11111">
  <Students>
    <Student ID="12345">
      <Exam Id="1">
        <Task id="1">2+2 = 4</Task>
      </Exam>
    </Student>
  </Students>
</Teacher>
