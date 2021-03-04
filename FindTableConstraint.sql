SELECT
	tblTableCon.CONSTRAINT_CATALOG,
	tblTableCon.CONSTRAINT_SCHEMA,
	tblTableCon.CONSTRAINT_NAME,
	tblTableCon.TABLE_CATALOG,
	tblTableCon.TABLE_SCHEMA,
	tblTableCon.TABLE_NAME,
	tblTableCon.CONSTRAINT_TYPE
FROM
	INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS tblTableCon
WHERE
	tblTableCon.TABLE_NAME = 'tblContract'
AND
	tblTableCon.CONSTRAINT_NAME = 'tblContract_PK'