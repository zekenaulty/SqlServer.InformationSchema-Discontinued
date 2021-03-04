--first result set Table Data
SELECT
	tblTables.TABLE_CATALOG,
	tblTables.TABLE_SCHEMA,
	tblTables.TABLE_NAME,
	tblTables.TABLE_TYPE
FROM
	INFORMATION_SCHEMA.TABLES AS tblTables
WHERE
	tblTables.TABLE_TYPE ='BASE TABLE'
	
	
--second result set table columns
SELECT
	tblCol.TABLE_CATALOG,
	tblCol.TABLE_SCHEMA,
	tblCol.TABLE_NAME,
	tblCol.COLUMN_NAME,
	tblCol.ORDINAL_POSITION,
	tblCol.COLUMN_DEFAULT,
	tblCol.IS_NULLABLE,
	tblCol.DATA_TYPE,
	tblCol.CHARACTER_MAXIMUM_LENGTH,
	tblCol.CHARACTER_OCTET_LENGTH,
	tblCol.NUMERIC_PRECISION,
	tblCol.NUMERIC_PRECISION_RADIX,
	tblCol.NUMERIC_SCALE
FROM 
	INFORMATION_SCHEMA.COLUMNS AS tblCol


--third result set views
SELECT
	TABLE_CATALOG,
	TABLE_SCHEMA,
	TABLE_NAME
FROM
	INFORMATION_SCHEMA.VIEWS AS tblView


--fourth result set view columns
SELECT
	tblViewCol.VIEW_CATALOG,
	tblViewCol.VIEW_SCHEMA,
	tblViewCol.VIEW_NAME,
	tblViewCol.TABLE_CATALOG,
	tblViewCol.TABLE_SCHEMA,
	tblViewCol.TABLE_NAME,
	tblViewCol.COLUMN_NAME
FROM
	INFORMATION_SCHEMA.VIEW_COLUMN_USAGE AS tblViewCol


--fifth result set routines
SELECT
	tblRoutine.SPECIFIC_CATALOG,
	tblRoutine.SPECIFIC_SCHEMA,
	tblRoutine.SPECIFIC_NAME,
	tblRoutine.ROUTINE_CATALOG,
	tblRoutine.ROUTINE_SCHEMA,
	tblRoutine.ROUTINE_NAME,
	tblRoutine.ROUTINE_TYPE,
	tblRoutine.DATA_TYPE,
	tblRoutine.CHARACTER_MAXIMUM_LENGTH,
	tblRoutine.CHARACTER_OCTET_LENGTH,
	tblRoutine.NUMERIC_PRECISION,
	tblRoutine.NUMERIC_PRECISION_RADIX,
	tblRoutine.NUMERIC_SCALE,
	tblRoutine.ROUTINE_BODY,
	tblRoutine.ROUTINE_DEFINITION,
	tblRoutine.IS_DETERMINISTIC,
	tblRoutine.IS_IMPLICITLY_INVOCABLE,
	tblRoutine.CREATED,
	tblRoutine.LAST_ALTERED
FROM
	INFORMATION_SCHEMA.ROUTINES AS tblRoutine
	
	
--sixth result set routines parameters
SELECT
	tblRoutineParam.SPECIFIC_CATALOG,
	tblRoutineParam.SPECIFIC_SCHEMA,
	tblRoutineParam.SPECIFIC_NAME,
	tblRoutineParam.ORDINAL_POSITION,
	tblRoutineParam.PARAMETER_MODE,
	tblRoutineParam.IS_RESULT,
	tblRoutineParam.AS_LOCATOR,
	tblRoutineParam.PARAMETER_NAME,
	tblRoutineParam.DATA_TYPE,
	tblRoutineParam.CHARACTER_MAXIMUM_LENGTH,
	tblRoutineParam.CHARACTER_OCTET_LENGTH,
	tblRoutineParam.NUMERIC_PRECISION,
	tblRoutineParam.NUMERIC_PRECISION_RADIX,
	tblRoutineParam.NUMERIC_SCALE
FROM
	INFORMATION_SCHEMA.PARAMETERS AS tblRoutineParam


--seventh result set routines columns
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
