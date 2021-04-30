import React, { useState, useRef } from 'react'
import { Button, Divider, Input, Select } from 'antd';
import { history } from 'umi';
import { PlusOutlined } from '@ant-design/icons';
import { PageContainer, FooterToolbar } from '@ant-design/pro-layout';
import ProTable, { ProColumns, ActionType, RequestData } from '@ant-design/pro-table';

import DynamicForm from '@/components/DynamicForm/DynamicForm';
import { ModelType, ControlType } from '@/components/DynamicForm/DynamicForm.d';

import { Course, Client } from '@/apis/API';
const { Option } = Select;



const category: React.FC<{}> = ({ }) => {
    //const [dataSource, setDatasource] = useState<Course[]>();
    const [creatFormVisible, handleCreateFormVisible] = useState<boolean>(false);
    const [updateFormVisible, handleUpdateFormVisible] = useState<boolean>(false);
    const [udpateFormData, handleUpdateFormData] = useState<{ data?: any, modelType?: ModelType[] }>({});


    const actionRef = useRef<ActionType>();
    const columns: ProColumns<Course>[] = [
        {
            title: '课程名',
            key: 'tags',
            hideInTable: true,
            dataIndex: 'tags',
            renderFormItem: (item, { type, defaultRender, ...rest }, form) => {
                if (type === 'form') {
                    return null;
                }
                const stateType = form.getFieldValue('state');
                if (stateType === 3) {
                    return <Input />;
                }

                return (
                    <Select
                        mode="tags"
                        size={'middle'}
                        placeholder="Please select"
                        defaultValue={['a10', 'c12']}
                        onChange={(value: any) => { console.log(value) }}
                        style={{ width: '100%' }}
                    >
                    </Select>
                );
            },
        },
        {
            title: '标题',
            dataIndex: 'name',
        }
        ,
        {
            title: '描述',
            dataIndex: 'description'
        },
        {
            title: '操作',
            dataIndex: 'option',
            valueType: 'option',
            render: (_, record) => (
                <>
                    <a
                        onClick={async () => {
                            await getCategoryAddOrUpdate(record.id)
                            handleUpdateFormVisible(true);
                        }}
                    >
                        编辑
                    </a>
                    <Divider type="vertical" />
                    <a onClick={async () => {
                        await handleDeleteCategory(record)
                        //actionRef.current.reload();
                    }}>删除</a>
                </>
            ),
        },
    ]

    var articleClient = new Client();

    const handleCreateCategory = async (data: any) => {
        return await articleClient.postCourseAddOrUpdate(data)
    }
    const getCategoryAddOrUpdate = async (id?: any) => {
        const res = await articleClient.getCourseAddOrUpdate(id)
        handleUpdateFormData({ data: res.data.modelData, modelType: res.data.modelType })
    }
    const handleDeleteCategory = async (data: any) => {
        await articleClient.deleteCourse(data.id)
    }

    return (
        <PageContainer>
            <ProTable<Course>
                actionRef={actionRef}
                columns={columns}
                rowKey="id"
                //dataSource={dataSource}
                toolBarRender={() => [
                    <Button type="primary" onClick={async () => {
                        //history.push('/course/edit')
                        await getCategoryAddOrUpdate()
                        handleCreateFormVisible(true)
                    }}><PlusOutlined />新建</Button>
                ]}
                request={async (params, sorter, filter) => {
                    var res = await articleClient.getCourseList(params.current, params.pageSize) 
                    const data: RequestData<Course> = { success: true, data: res.data, total: res.count, key: ['id'] };
                    return data;
                }}
            >
            </ProTable>
            {creatFormVisible && (
                <DynamicForm key="createForm" onSubmit={async (data) => {
                    await handleCreateCategory(data)
                    handleCreateFormVisible(false);
                    actionRef.current.reload();
                }}
                    onCancel={() => {
                        handleCreateFormVisible(false);
                    }}
                    data={udpateFormData?.data}
                    longIdKey='LongCatgoryId'
                    modelTypes={udpateFormData?.modelType || []}
                    formModelVisible={creatFormVisible}>

                </DynamicForm>)}
            {updateFormVisible && (
                <DynamicForm key="cupdateForm" onSubmit={async (data) => {
                    await handleCreateCategory(data)
                    handleUpdateFormVisible(false);
                    actionRef.current.reload();
                }}
                    onCancel={() => {
                        handleUpdateFormVisible(false);
                        handleUpdateFormData({});
                    }}
                    data={udpateFormData?.data}
                    modelData={udpateFormData?.data}
                    longIdKey='LongCatgoryId'
                    modelTypes={udpateFormData?.modelType || []}
                    formModelVisible={updateFormVisible}>

                </DynamicForm>
            )}
        </PageContainer >
    )
}

export default category;