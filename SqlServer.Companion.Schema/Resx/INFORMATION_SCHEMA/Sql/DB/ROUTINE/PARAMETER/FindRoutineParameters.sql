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
WHERE
	tblRoutineParam.SPECIFIC_NAME = '@RoutineName'