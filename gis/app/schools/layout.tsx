"use client";

const SchoolsLayout: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  return (
    <div className="flex justify-center items-center bg-gray-50 dark:bg-gray-900">
      {/* Fixed Size Container */}
      <div className="w-[1200px] bg-gray-100 dark:bg-gray-800 shadow-lg rounded-md overflow-hidden">
        {children}
      </div>
    </div>
  );
};

export default SchoolsLayout;
