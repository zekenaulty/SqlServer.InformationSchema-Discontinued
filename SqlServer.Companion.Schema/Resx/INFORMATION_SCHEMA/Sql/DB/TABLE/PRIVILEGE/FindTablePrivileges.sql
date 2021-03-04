SELECT 
	tblTablePriv.GRANTOR,
	tblTablePriv.GRANTEE,
	tblTablePriv.TABLE_CATALOG,
	tblTablePriv.TABLE_SCHEMA,
	tblTablePriv.TABLE_NAME,
	tblTablePriv.PRIVILEGE_TYPE,
	tblTablePriv.IS_GRANTABLE
FROM
	INFORMATION_SCHEMA.TABLE_PRIVILEGES AS tblTablePriv
WHERE
	tblTablePriv.TABLE_NAME = '@TableName'