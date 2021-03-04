			if(null != filters && filters.Count > 1)
			{
				CodeArrayCreateExpression pa = GenrateCreateSqlParamArrayStatement(filters, true);
				qry.Statements.Add(
					new CodeVariableDeclarationStatement(
					typeof(System.Data.SqlClient.SqlParameter[]),
					"param",
					pa)
					);
				exc = new CodeAssignStatement(new CodeVariableReferenceExpression("ret"),
					new CodeMethodInvokeExpression(
						new CodeVariableReferenceExpression(@namespace + "." + tbl.TableName), 
						(findMany == true) ? "BuildArray" : "Build", 
						new CodeExpression[]{
												new CodeMethodInvokeExpression(
													new CodePropertyReferenceExpression(
														new CodePropertyReferenceExpression(
															new CodeVariableReferenceExpression("conn"),
															"DbAccessor"),
														"ExecuteProcedure"),
													"ExecuteReader",
													new CodeExpression[]{
																			new CodePrimitiveExpression(proc_name), 
																			new CodeVariableReferenceExpression("param")}
												)}
					));
			}
			else if(null != filters && filters.Count == 1)
			{
				qry.Statements.Add(
					new CodeVariableDeclarationStatement(
					typeof(System.Data.SqlClient.SqlParameter), 
					"param", 
					new CodeObjectCreateExpression(
					typeof(System.Data.SqlClient.SqlParameter),
					new CodeExpression[]{
											new CodePrimitiveExpression("@" + ((SqlDbTableField)filters[0]).ColumnName),
											new CodeVariableReferenceExpression(n)
										}
					)));
				exc = new CodeAssignStatement(new CodeVariableReferenceExpression("ret"),
					new CodeMethodInvokeExpression(
					new CodeVariableReferenceExpression(@namespace + "." + tbl.TableName), 
					(findMany == true) ? "BuildArray" : "Build", 
					new CodeExpression[]{
											new CodeMethodInvokeExpression(
												new CodePropertyReferenceExpression(
													new CodePropertyReferenceExpression(
														new CodeVariableReferenceExpression("conn"),
														"DbAccessor"),
													"ExecuteProcedure"),
												"ExecuteReader",
												new CodeExpression[]{
																		new CodePrimitiveExpression(proc_name), 
																		new CodeVariableReferenceExpression("param")}
											)}
					));
			}
			else
			{
				exc = new CodeAssignStatement(new CodeVariableReferenceExpression("ret"),
					new CodeMethodInvokeExpression(
					new CodeVariableReferenceExpression(@namespace + "." + tbl.TableName), 
					(findMany == true) ? "BuildArray" : "Build", 
					new CodeExpression[]{
											new CodeMethodInvokeExpression(
												new CodePropertyReferenceExpression(
													new CodePropertyReferenceExpression(
														new CodeVariableReferenceExpression("conn"),
														"DbAccessor"),
													"ExecuteProcedure"),
												"ExecuteReader",
												new CodeExpression[]{new CodePrimitiveExpression(proc_name)}
											)}
					));
			}