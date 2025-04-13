"use client";

import Sidebar from "../components/Sidebar";

const StudentsLayout: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  return (
    <div className="flex">
      <Sidebar />
      <div className="flex-1 p-4 bg-gray-100">{children}</div>
    </div>
  );
};

export default StudentsLayout;
