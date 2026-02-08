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
<Teachers>
    <Teacher ID = "11111">
        <Students>
            <Student ID="1111">
                <Exam Id="1">
                    <Task id = "1"> 2+3/6-4 = 74 </Task >
                    <Task id = "2"> 6*2+3-4 = 22 </Task >
                </Exam>
            </Student>
            <Student ID="1112">
                <Exam Id="2">
                    <Task id = "1"> 2+3/6-4 = 74 </Task >
                    <Task id = "2"> 6*2+3-4 = 11 </Task >
                </Exam>
            </Student>
            <Student ID="1113">
                <Exam Id="3">
                    <Task id = "1"> 2+3/6-4 = -1.5 </Task >
                    <Task id = "2"> 6*2+3-4 = 11 </Task >
                </Exam>
            </Student>
        </Students >
    </Teacher>
    <Teacher ID = "22222">
        <Students>
            <Student ID="1111">
                <Exam Id="1">
                    <Task id = "1"> 2+3/6-4 = 74 </Task >
                    <Task id = "2"> 6*2+3-4 = 22 </Task >
                </Exam>
            </Student>
            <Student ID="1112">
                <Exam Id="2">
                    <Task id = "1"> 2+3/6-4 = 74 </Task >
                    <Task id = "2"> 6*2+3-4 = 11 </Task >
                </Exam>
            </Student>
            <Student ID="1113">
                <Exam Id="3">
                    <Task id = "1"> 2+3/6-4 = -1.5 </Task >
                    <Task id = "2"> 6*2+3-4 = 11 </Task >
                </Exam>
            </Student>
        </Students >
    </Teacher>
</Teachers>
