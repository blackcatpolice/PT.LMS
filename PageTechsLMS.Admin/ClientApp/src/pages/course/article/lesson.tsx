import React, { useState, useEffect } from 'react';
import { Row, Col, Card } from 'antd';
import { PageContainer } from '@ant-design/pro-layout'
import LessonForm from './components/lessonForm'
import LessonSectionList from './components/lessonSectionList'


const page: React.FC<{}> = () => {
    const [courseId, setCourseId] = useState<any>()
    const [sectionId, setSection] = useState();

    useEffect(() => {
        setCourseId(1)
    }, [])
    return (
        <PageContainer>
            <Row>
                <Col span={courseId ? 12 : 24}>
                    <Card>
                        <LessonForm></LessonForm>
                    </Card>
                </Col>
                {courseId && <Col span={12}>
                    <Card>
                      
                        <LessonSectionList courseId={courseId} sectionId={sectionId}></LessonSectionList>
                    </Card>
                </Col>}
            </Row>
        </PageContainer >
    );
}

export default page;