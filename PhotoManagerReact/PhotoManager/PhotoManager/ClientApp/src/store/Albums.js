const requestAlbumsType = 'REQUEST_ALBUMS';
const receiveAlbumsType = 'RECEIVE_ALBUMS';

const initAlbumsType = 'INIT_ALBUMS';

const viewAlbumType = 'VIEW_ALBUM';
const editAlbumType = 'EDIT_ALBUM';
const removeAlbumType = 'REMOVE_ALBUM';
const addAlbumType = 'ADD_ALBUM';
const goToAlbumType = 'GO_TO_ALBUM';

const selectAlbumType = 'SELECT_ALBUM';

const beforeRedirectType = 'BEFORE_REDIRECT_TO_PHOTOS';

const removeSuccessType = 'ALBUM_REMOVE_SUCCESS'
const removeErrorType = 'ALBUM_REMOVE_ERROR'

const createAlertType = 'CREATE_ALERT';

const pageSize = 9;

const initialState = {
    albums: [],
    albumId: "",

    redirectToEdit: false,
    redirectToAdd: false,
    redirectToView: false,
    redirectToAlbum: false,
    redirectToRemove: false,

    isLoading: false,
    hasMore: true,
    pageIndex: 0,

    alertMessage: null,
    alertId: 0,
    alertType: "",
    currentDate: "",
    refresh: false
};

export const actionCreators = {

    requestAlbums: pageIndex => (dispatch) => {
        dispatch({ type: requestAlbumsType, pageIndex });
        fetch(`api/Albums/GetAllAlbums?pageIndex=${pageIndex}&pageSize=${pageSize}`)
            .then(function (response) {
                if (response.ok) {
                    response.json().then(albums => {
                        dispatch({ type: receiveAlbumsType, pageIndex, albums })
                    })
                }
                else {
                    dispatch({ type: createAlertType })
                    dispatch({ type: receiveAlbumsType, pageIndex, albums: [] })
                }
            })
    },

    onInitAlbums: () => ({ type: initAlbumsType }),
    onSelectImage: (index) => ({ type: selectAlbumType, index }),
    onAlbumEditClick: () => ({ type: editAlbumType }),
    onAlbumViewClick: () => ({ type: viewAlbumType }),
    onAlbumAddClick: () => ({ type: addAlbumType }),
    onAlbumRemoveClick: () => ({ type: removeAlbumType }),
    onAlbumThumbnailClick: (index) => ({ type: goToAlbumType, index }),
    onBeforeRedirect: () => ({ type: beforeRedirectType }),
    onRemoveSuccess: () => ({ type: removeSuccessType }),
    onRemoveError: (message) => ({ type: removeErrorType, message })
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === initAlbumsType) {
        return {
            ...state,
            currentDate: new Date().getTime()
        };
    }

    if (action.type === requestAlbumsType) {
        return {
            ...state,
            isLoading: true,
            refresh: false
        };
    }

    if (action.type === receiveAlbumsType) {
        return {
            ...state,
            pageIndex: action.albums.length > 0 ? state.pageIndex + 1 : state.pageIndex,
            hasMore: action.albums.length === pageSize,
            albums: [...state.albums || [], ...action.albums],
            isLoading: false,
            refresh: false
        };
    }

    if (action.type === viewAlbumType) {
        if (state.albums.filter(album => album.isSelected === true).length === 1)
            return {
                ...state,
                albumId: state.albums.filter(album => album.isSelected === true)[0].albumId,
                redirectToAlbum: true
            }
        if (state.albums.filter(album => album.isSelected).length === 0) {
            return {
                ...state,
                alertMessage: "Please select albums to view",
                alertId: state.alertId + 1,
                alertType: "info"
            }
        }
        return {
            ...state,
            alertMessage: "Please select one album to view",
            alertId: state.alertId + 1,
            alertType: "info"
        }
    }

    if (action.type === removeAlbumType) {
        if (state.albums.filter(album => album.isSelected).length === 0) {
            return {
                ...state,
                alertMessage: "Please select albums to remove",
                alertId: state.alertId + 1,
                alertType: "info"
            }
        }
        return {
            ...state,
            albumId: 0,
            redirectToRemove: true
        }
    }

    if (action.type === editAlbumType) {
        if (state.albums.filter(album => album.isSelected).length === 1)
            return {
                ...state,
                albumId: state.albums.filter(album => album.isSelected)[0].albumId,
                redirectToEdit: true
            }
        else {
            if (state.albums.filter(album => album.isSelected).length === 0) {
                return {
                    ...state,
                    alertMessage: "Please select albums to edit",
                    alertId: state.alertId + 1,
                    alertType: "info"
                }
            }
            return {
                ...state,
                alertMessage: "Please select one album to edit",
                alertId: state.alertId + 1,
                alertType: "info"
            }
        }
    }

    if (action.type === addAlbumType) {
        return {
            ...state,
            albumId: 0,
            redirectToAdd: true
        }
    }

    if (action.type === goToAlbumType) {
        return {
            ...state,
            albumId: state.albums[action.index].albumId,
            redirectToAlbum: true
        }
    }

    if (action.type === selectAlbumType) {
        return {
            ...state,
            albums: state.albums.map((album, i) => ({
                ...album,
                isSelected: i !== action.index ? album.isSelected : (album.hasOwnProperty("isSelected") ? !album.isSelected : true)
            }))
        }
    }

    if (action.type === beforeRedirectType) {
        return {
            ...state,
            redirectToAlbum: false,
            redirectToEdit: false,
            redirectToAdd: false,
            redirectToView: false,
            redirectToRemove: false,
            albumId: "",
            pageIndex: 0,
            alertMessage: null,
            albums: []
        }
    }

    if (action.type === createAlertType) {
        return {
            ...state,
            alertMessage: "Error getting albums",
            alertId: state.alertId + 1,
            alertType: "danger"
        }
    }

    if (action.type === removeSuccessType) {
        return {
            pageIndex: 0,
            postSaveChanges: false,
            alertMessage: "The albums have been removed successfully",
            alertId: state.alertId + 1,
            alertType: "success",
            redirectToRemove: false,
            albums: [],
            refresh: true
        }
    }

    if (action.type === removeErrorType) {
        return {
            ...state,
            postSaveChanges: false,
            alertMessage: action.message,
            alertId: action.message === state.alertMessage ? state.alertId : state.alertId + 1,
            alertType: "warning"
        }
    }

    return state;
}