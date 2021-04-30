## 数据表结构<!-- {docsify-ignore} -->

### Member


#### MemberAccount
| 列    | 类型   | 备注   |
| ----- | ------ | ------ |
| Id    | Guid   | 用户Id |
| Phone | String | 电话   |


#### MemberBindWechat
| 列              | 类型   | 备注         |
| --------------- | ------ | ------------ |
| Id              | Guid   | 用户Id       |
| OpenId          | String | 用户名       |
| Uid             | String | 用户名       |
| MemberAccountId | Guid   | 用户账户外键 |



#### MemberInfo
| 列              | 类型   | 备注         |
| --------------- | ------ | ------------ |
| Id              | Guid   | 用户Id       |
| Name            | String | 用户名       |
| Avatar          | String | 头像         |
| MemberAccountId | Guid   | 用户账户外键 |

#### MemberStudentInfo
| 列              | 类型 | 备注         |
| --------------- | ---- | ------------ |
| Id              | Guid | 用户Id       |
| MemberAccountId | Guid | 用户账户外键 |


#### MemberTeacherInfo
| 列表            | 类型 | 备注         |
| --------------- | ---- | ------------ |
| Id              | Guid | 用户Id       |
| MemberAccountId | Guid | 用户账户外键 |

### Course 

#### Course-Category
| 列          | 类型   | 备注   |
| ----------- | ------ | ------ |
| Id          | Guid   | 用户Id |
| Name        | Guid   | 课程名 |
| CoverImg    | String | 封面   |
| Description | String | 描述   |
 
#### Course 
| 列               | 类型   | 备注                       |
| ---------------- | ------ | -------------------------- |
| Id               | Guid   | 用户Id                     |
| Name             | Guid   | 课程名                     |
| Accessable       | String | 免费课程/付费课程/私有课程 |
| Cover            | String | 封面                       |
| Description      | String | 描述                       |
| TotalTime        | String | 总时长                     |
| CourseCategoryId | Guid   | 分类外键Id                 |


#### Course-Tag-List
| 列               | 类型   | 备注                       |
| ---------------- | ------ | -------------------------- |
| Id               | Guid   | 用户Id                     |
| Name             | Guid   | 课程名                     |
| Accessable       | String | 免费课程/付费课程/私有课程 |
| CourseCategoryId | String | 分类外键Id                 |

#### Course
| 列         | 类型   | 备注              |
| ---------- | ------ | ----------------- |
| Id         | Guid   | 用户Id            |
| Name       | String | 课程名            |
| Accessable | String | 免费课程/私有课程 |

#### Course-Tag-Mapping
| 列       | 类型   | 备注     |
| -------- | ------ | -------- |
| Id       | Guid   | 用户Id   |
| Name     | String | 课程名   |
| CourseId | Guid   | 课程外键 |

#### Course-Item
| 列       | 类型 | 备注     |
| -------- | ---- | -------- |
| Id       | Guid | 用户Id   |
| Name     | Guid | 课程名   |
| CourseId | Guid | 课程外键 |

#### Course-Item-Section
| 列           | 类型 | 备注         |
| ------------ | ---- | ------------ |
| Id           | Guid | 用户Id       |
| Name         | Guid | 课程名       |
| CourseId     | Guid | 用户账户外键 |
| CourseItemId | Guid | 课程项外键   |

#### Course-Student
| 列       | 类型 | 备注     |
| -------- | ---- | -------- |
| Id       | Guid | 用户Id   |
| CourseId | Guid | 课程外键 |
| MemberId | Guid | 用户外键 |

#### Course-Teacher
| 列       | 类型 | 备注     |
| -------- | ---- | -------- |
| Id       | Guid | 用户Id   |
| CourseId | Guid | 课程外键 |
| MemberId | Guid | 用户外键 |


#### Material library
| 列       | 类型 | 备注     |
| -------- | ---- | -------- |
| Id       | Guid | 用户Id   |
| CourseId | Guid | 课程外键 |
| MemberId | Guid | 用户外键 |


### Paths
| 列          | 类型   | 备注     |
| ----------- | ------ | -------- |
| Id          | Guid   | 用户Id   |
| Name        | String | 路径名称 |
| CoverImg    | String | 封面     |
| Description | String | 描述     |
| TotalTime   | String | 总时长   |

#### Paths-Item
| 列       | 类型   | 备注     |
| -------- | ------ | -------- |
| Id       | Guid   | 用户Id   |
| Name     | String | 名字     |
| CourseId | Guid   | 课程外键 |


### Quiz
| 列         | 类型   | 备注     |
| ---------- | ------ | -------- |
| Id         | Guid   | 用户Id   |
| Name       | String | 名字     |
| CourseId   | Guid   | 课程外键 |
| TotalTime  | String | 总时长   |
| TotalScore | String | 总分     |

#### Quiz-Item
| 列         | 类型   | 备注     |
| ---------- | ------ | -------- |
| Id         | Guid   | 用户Id   |
| Name       | String | 名字     |
| CourseId   | Guid   | 课程外键 |
| QuizId     | Guid   | 试题Id   |
| Score      | String | 分数     |
| Time       | String | 时长     |
| Time       | String | 时长     |
| TotalScore | String | 总分     |

#### Quiz-Result
| 列         | 类型   | 备注     |
| ---------- | ------ | -------- |
| Id         | Guid   | 用户Id   |
| QuizId     | Guid   | 试题Id   |
| MemberId   | Guid   | 用户Id   |
| CourseId   | Guid   | 课程外键 |
| TotalTime  | String | 总时长   |
| TotalScore | String | 总分     |

#### Quiz-Item-Result
| 列         | 类型   | 备注     |
| ---------- | ------ | -------- |
| Id         | Guid   | 用户Id   |
| QuizId     | Guid   | 试题Id   |
| QuizItemId | Guid   | 试题项Id |
| MemberId   | Guid   | 用户Id   |
| CourseId   | Guid   | 课程外键 |
| Time       | String | 时长     |
| Score      | String | 分数     |


### Order
| 列         | 类型     | 备注     |
| ---------- | -------- | -------- |
| Id         | Guid     | 用户Id   |
| MemberId   | Guid     | 用户Id   |
| CourseId   | Guid     | 课程外键 |
| CreateTime | DateTime | 创建时间 |
| Price      | double   | 价格     |
| Status     | String   | 状态     |

### PayLog

| 列         | 类型     | 备注     |
| ---------- | -------- | -------- |
| Id         | Guid     | 用户Id   |
| MemberId   | Guid     | 用户Id   |
| CreateTime | DateTime | 创建时间 |
| Price      | double   | 价格     |
| Status     | String   | 状态     |
