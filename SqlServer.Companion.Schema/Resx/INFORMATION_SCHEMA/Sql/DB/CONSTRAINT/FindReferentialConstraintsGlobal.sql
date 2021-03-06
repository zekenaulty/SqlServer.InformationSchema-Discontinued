SELECT
	tblRefCon.CONSTRAINT_CATALOG,
	tblRefCon.CONSTRAINT_SCHEMA,
	tblRefCon.CONSTRAINT_NAME,
	tblRefCon.UNIQUE_CONSTRAINT_CATALOG,
	tblRefCon.UNIQUE_CONSTRAINT_SCHEMA,
	tblRefCon.UNIQUE_CONSTRAINT_NAME,
	tblRefCon.UPDATE_RULE,
	tblRefCon.DELETE_RULE
FROM
	INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS AS tblRefCon
WHERE
	tblRefCon.CONSTRAINT_NAME = '@ConstraintName'
