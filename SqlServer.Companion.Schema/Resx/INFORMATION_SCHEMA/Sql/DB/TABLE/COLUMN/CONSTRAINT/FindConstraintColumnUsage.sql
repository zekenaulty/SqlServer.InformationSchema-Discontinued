SELECT
	tblColConUsg.TABLE_CATALOG,
	tblColConUsg.TABLE_SCHEMA,
	tblColConUsg.TABLE_NAME,
	tblColConUsg.COLUMN_NAME,
	tblColConUsg.CONSTRAINT_CATALOG,
	tblColConUsg.CONSTRAINT_SCHEMA,
	tblColConUsg.CONSTRAINT_NAME
FROM
	INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE AS tblColConUsg
WHERE
	tblColConUsg.CONSTRAINT_NAME = '@ConstraintName'