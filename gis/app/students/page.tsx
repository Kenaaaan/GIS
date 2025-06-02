"use client";

import React, { useEffect, useState } from "react";
import {
  getUceniciDataLimit,
  GetUcenikDataByLocation,
} from "../../services/apiClient";
import { Student } from "../../types/Student";

const StudentsPage: React.FC = () => {
  const [students, setStudents] = useState<Student[]>([]);
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    const fetchStudents = async () => {
      try {
        const data = await getUceniciDataLimit(30);
        setStudents(data);
        console.log(data);
      } catch (error) {
        console.error("Error fetching students:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchStudents();
  }, []);

  return (
    <div className="min-h-screen bg-gray-50 dark:bg-gray-900 p-8">
      <div className="max-w-[1200px] mx-auto bg-gray-100 dark:bg-gray-800 shadow-lg rounded-md overflow-hidden">
        <div className="p-8 sm:p-4 flex flex-col">
          <h1 className="text-2xl font-bold mb-4 text-gray-800 dark:text-gray-200">
            Students
          </h1>
          <p className="text-gray-700 dark:text-gray-300 mb-6">
            Below is the list of students fetched from the API.
          </p>
          {loading ? (
            <p className="text-gray-500 dark:text-gray-400">
              Loading students...
            </p>
          ) : (
            <div className="overflow-x-auto max-h-[450px]">
              <table className="table-auto w-full bg-gray-100 dark:bg-gray-800 text-gray-800 dark:text-gray-200 text-sm shadow-md rounded-md">
                <thead>
                  <tr className="bg-gray-200 dark:bg-gray-700 text-left">
                    <th className="px-4 py-2">Teritorija</th>
                    <th className="px-4 py-2">Starost</th>
                    <th className="px-4 py-2">Spol</th>
                    <th className="px-4 py-2">Ukupno</th>
                    <th className="px-4 py-2">Ne Skoluje Se</th>
                    <th className="px-4 py-2">Predskolsko Obrazovanje</th>
                    <th className="px-4 py-2">Osnovna Skola</th>
                    <th className="px-4 py-2">Srednja Skola</th>
                    <th className="px-4 py-2">
                      Specijalizacija Poslije Srednje
                    </th>
                    <th className="px-4 py-2">Visa Skola</th>
                    <th className="px-4 py-2">Stari Program Osnovne</th>
                    <th className="px-4 py-2">Stari Program Specijalisticke</th>
                    <th className="px-4 py-2">Stari Program Magistarske</th>
                    <th className="px-4 py-2">Stari Program Doktorske</th>
                    <th className="px-4 py-2">Program Bolonja I</th>
                    <th className="px-4 py-2">Program Bolonja II</th>
                    <th className="px-4 py-2">Program Bolonja Integrisani</th>
                    <th className="px-4 py-2">Program Bolonja III</th>
                  </tr>
                </thead>
                <tbody>
                  {students.map((student) => (
                    <tr
                      key={student.id}
                      className="border-t border-gray-300 dark:border-gray-600"
                    >
                      <td className="px-4 py-2">{student.teritorija}</td>
                      <td className="px-4 py-2">{student.starost}</td>
                      <td className="px-4 py-2">{student.spol}</td>
                      <td className="px-4 py-2">{student.ukupno}</td>
                      <td className="px-4 py-2">{student.neSkolujeSe}</td>
                      <td className="px-4 py-2">
                        {student.predskolskoObrazovanje}
                      </td>
                      <td className="px-4 py-2">{student.osnovnaSkola}</td>
                      <td className="px-4 py-2">{student.srednjaSkola}</td>
                      <td className="px-4 py-2">
                        {student.specijalizacijaPoslijeSrednje}
                      </td>
                      <td className="px-4 py-2">{student.visaSkola}</td>
                      <td className="px-4 py-2">
                        {student.stariProgramOsnovne}
                      </td>
                      <td className="px-4 py-2">
                        {student.stariProgramSpecijalisticke}
                      </td>
                      <td className="px-4 py-2">
                        {student.stariProgramMagistarske}
                      </td>
                      <td className="px-4 py-2">
                        {student.stariProgramDoktorske}
                      </td>
                      <td className="px-4 py-2">{student.programBolonjaI}</td>
                      <td className="px-4 py-2">{student.programBolonjaII}</td>
                      <td className="px-4 py-2">
                        {student.programBolonjaIntegrisani}
                      </td>
                      <td className="px-4 py-2">{student.programBolonjaIII}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default StudentsPage;
