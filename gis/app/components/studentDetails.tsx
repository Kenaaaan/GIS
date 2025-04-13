import React from "react";
import { Student } from "../../types/Student";

interface StudentDetailsProps {
  student: Student;
}

const StudentDetails: React.FC<StudentDetailsProps> = ({ student }) => {
  return (
    <div>
      <h1>Student Details</h1>
      <table>
        <tbody>
          <tr>
            <td>Starost</td>
            <td>{student.starost}</td>
          </tr>
          <tr>
            <td>Spol</td>
            <td>{student.spol}</td>
          </tr>
          <tr>
            <td>Ne Školuje Se</td>
            <td>{student.neSkolujeSe}</td>
          </tr>
          <tr>
            <td>Predškolsko Obrazovanje</td>
            <td>{student.predskolskoObrazovanje}</td>
          </tr>
          <tr>
            <td>Osnovna Škola</td>
            <td>{student.osnovnaSkola}</td>
          </tr>
          <tr>
            <td>Srednja Škola</td>
            <td>{student.srednjaSkola}</td>
          </tr>
          <tr>
            <td>Specijalizacija Poslije Srednje</td>
            <td>{student.specijalizacijaPoslijeSrednje}</td>
          </tr>
          <tr>
            <td>Viša Škola</td>
            <td>{student.visaSkola}</td>
          </tr>
          <tr>
            <td>Stari Program Osnovne</td>
            <td>{student.stariProgramOsnovne}</td>
          </tr>
          <tr>
            <td>Stari Program Specijalističke</td>
            <td>{student.stariProgramSpecijalisticke}</td>
          </tr>
          <tr>
            <td>Stari Program Magistarske</td>
            <td>{student.stariProgramMagistarske}</td>
          </tr>
          <tr>
            <td>Stari Program Doktorske</td>
            <td>{student.stariProgramDoktorske}</td>
          </tr>
          <tr>
            <td>Program Bolonja I</td>
            <td>{student.programBolonjaI}</td>
          </tr>
          <tr>
            <td>Program Bolonja II</td>
            <td>{student.programBolonjaII}</td>
          </tr>
          <tr>
            <td>Program Bolonja Integrisani</td>
            <td>{student.programBolonjaIntegrisani}</td>
          </tr>
          <tr>
            <td>Program Bolonja III</td>
            <td>{student.programBolonjaIII}</td>
          </tr>
        </tbody>
      </table>
    </div>
  );
};

export default StudentDetails;
