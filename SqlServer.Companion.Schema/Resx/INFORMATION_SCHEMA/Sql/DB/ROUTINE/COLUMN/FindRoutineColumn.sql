SELECT
	tblRoutineCol.TABLE_CATALOG,
	tblRoutineCol.TABLE_SCHEMA,
	tblRoutineCol.TABLE_NAME,
	tblRoutineCol.COLUMN_NAME,
	tblRoutineCol.ORDINAL_POSITION,
	tblRoutineCol.COLUMN_DEFAULT,
	tblRoutineCol.IS_NULLABLE,
	tblRoutineCol.DATA_TYPE,
	tblRoutineCol.CHARACTER_MAXIMUM_LENGTH,
	tblRoutineCol.CHARACTER_OCTET_LENGTH,
	tblRoutineCol.NUMERIC_PRECISION,
	tblRoutineCol.NUMERIC_PRECISION_RADIX,
	tblRoutineCol.NUMERIC_SCALE
FROM 
	INFORMATION_SCHEMA.ROUTINE_COLUMNS AS tblRoutineCol
WHERE
	tblRoutineCol.TABLE_NAME = '@TableName'
AND
	tblRoutineCol.COLUMN_NAME = '@ColumnName'