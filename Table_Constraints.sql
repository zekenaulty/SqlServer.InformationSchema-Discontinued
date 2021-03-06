SELECT
	tblTblCon.TABLE_NAME,
	tblTblCon.CONSTRAINT_TYPE,
	tblTblConUsage.COLUMN_NAME,
	tblTblCon.CONSTRAINT_NAME 
FROM 
	INFORMATION_SCHEMA.TABLE_CONSTRAINTS  As tblTblCon
INNER JOIN 
	INFORMATION_SCHEMA.CONSTRAINT_TABLE_USAGE  AS tblTblConUsage
ON 
	tblTblCon.CONSTRAINT_NAME = tblTblConUsage.CONSTRAINT_NAME
ORDER BY tblTblCon.CONSTRAINT_NAME