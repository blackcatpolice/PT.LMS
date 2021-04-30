import defaultRoute from './routes/route.default';
import systemRoute from './routes/route.system';
import courseRoute from './routes/router.course';
import financeRoute from './routes/route.finance';
import memberRoute from './routes/route.member';


export default [
    ...defaultRoute,
    ...courseRoute,
    ...memberRoute,
    ...financeRoute,
    ...systemRoute
]