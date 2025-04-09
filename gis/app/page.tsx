"use client";

import { useState, useEffect } from "react";
import { Student } from "../types/Student";
import StudentDetails from "./components/studentDetails";

const App: React.FC = () => {
  const [student, setStudent] = useState<Student | null>(null);

  return (
    <div>
      <h1>Student Information</h1>
      {student ? (
        <StudentDetails student={student} />
      ) : (
        <p>Loading student data...</p>
      )}
    </div>
  );
};

export default App;
