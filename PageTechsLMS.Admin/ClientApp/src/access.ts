// src/access.ts
export default function access(initialState: { currentUser?: API.CurrentUser | undefined, roles?: string[] }) {
  const { currentUser } = initialState || {};
  const { roles } = initialState || {};
  return {
    canAdmin: roles && roles.indexOf('admin'),
    checkPermission: true,
    anonymous: true
  };
}
