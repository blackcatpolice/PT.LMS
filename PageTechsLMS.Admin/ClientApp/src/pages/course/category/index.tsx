import React, { useState, useRef } from 'react'
import { history } from 'umi';
import { Button, Divider } from 'antd';
import { PlusOutlined } from '@ant-design/icons';
import { PageContainer, FooterToolbar } from '@ant-design/pro-layout';
import ProTable, { ProColumns, ActionType, RequestData } from '@ant-design/pro-table';

import DynamicForm from '@/components/DynamicForm/DynamicForm';
import { ModelType, ControlType } from '@/components/DynamicForm/DynamicForm.d';

import { Category, Client } from '@/apis/API';



const category: React.FC<{}> = ({ }) => {
      
    const [dataSource, setDatasource] = useState<Category[]>();
    const [creatFormVisible, handleCreateFormVisible] = useState<boolean>(false);
    const [updateFormVisible, handleUpdateFormVisible] = useState<boolean>(false);
    const [udpateFormData, handleUpdateFormData] = useState<{ data?: any, modelType?: ModelType[] }>({});

    const actionRef = useRef<ActionType>();
    const columns: ProColumns<Category>[] = [
        {
            title: '类名',
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
                        actionRef.current.reload();
                    }}>删除</a>
                </>
            ),
        },
    ]

    var categoryClient = new Client();

    const handleCreateCategory = async (data: any) => {
        return await categoryClient.postCategoryAddOrUpdate(data)
    }
    const getCategoryAddOrUpdate = async (id?: any) => {
        const res = await categoryClient.getCategoryAddOrUpdate(id)
        handleUpdateFormData({ data: res.data.modelData, modelType: res.data.modelType })
    }
    const handleDeleteCategory = async (data: any) => {
        await categoryClient.deleteCategory(data.id)
    }

    return (
        <PageContainer>
            <ProTable<Category>
                actionRef={actionRef}
                columns={columns}
                rowKey="id"
                dataSource={dataSource}
                toolBarRender={() => [
                    <Button type="primary" onClick={async () => {
                        //history.push('/course/list')
                        await getCategoryAddOrUpdate()
                        handleCreateFormVisible(true)
                    }}><PlusOutlined />新建</Button>
                ]}
                request={async (params, sorter, filter) => {
                    var res = await categoryClient.getCategoryList(params.current, params.pageSize)
                    const data: RequestData<Category> = { success: true, data: res.data, total: res.count, key: ['id'] };
                    return data;
                }}
            >
            </ProTable>
            {creatFormVisible && (
                <DynamicForm onSubmit={async (data) => {
                    await handleCreateCategory(data)
                    handleCreateFormVisible(false);
                    actionRef.current.reload();
                }}
                    onCancel={() => {
                        handleCreateFormVisible(false);
                        handleUpdateFormData({});
                        actionRef.current.reload();
                    }}
                    data={udpateFormData?.data}
                    longIdKey='LongParentId'
                    modelTypes={udpateFormData?.modelType || []}
                    formModelVisible={creatFormVisible}>

                </DynamicForm>)}
            {updateFormVisible && (
                <DynamicForm onSubmit={async (data) => {
                    await handleCreateCategory(data)
                    handleUpdateFormVisible(false);
                    actionRef.current.reload();
                }}
                    onCancel={() => {
                        handleUpdateFormVisible(false);
                        handleUpdateFormData({});
                        actionRef.current.reload();
                    }}
                    data={udpateFormData?.data}
                    modelData={udpateFormData?.data}
                    longIdKey='LongParentId'
                    modelTypes={udpateFormData?.modelType || []}
                    formModelVisible={updateFormVisible}>

                </DynamicForm>
            )}
        </PageContainer >
    )
}

export default category;