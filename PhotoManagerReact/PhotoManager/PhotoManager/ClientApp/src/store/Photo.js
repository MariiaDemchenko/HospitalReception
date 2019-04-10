const shotDateChangedType = 'SHOT_DATE_CHANGED';
const photoNameChangedType = 'PHOTO_NAME_CHANGED';
const cameraChangedType = 'CAMERA_CHANGED'
const lensFocalLengthChangedType = 'LENS_FOCAL_LENGTH_CHANGED'

const saveChangesType = 'PHOTO_EDIT_SAVE_CHANGES';
const removeSelectedPhotoType = 'PHOTO_REMOVE_SELECTED';
const onDropType = 'ON_DROP_PHOTO';
const onInitType = 'ON_INIT';
const onInitAlbumType = 'ON_INIT_ALBUM'

const onDropToAddType = 'ON_DROP_ADD';
const setCommonDescriptionType = 'SET_COMMON_DESCRIPTION'

const onEditPhotoType = 'EDIT_SELECTED_PHOTOS';

const beforeRedirectType = 'BEFORE_REDIRECT_P';

const updateSuccessType = 'PHOTO_UPDATE_SUCCESS'
const updateErrorType = 'PHOTO_UPDATE_ERROR'

const initialState = {
    selectedPhotos: [],
    selectedAlbum: {},
    postSaveChanges: false,
    redirectToEdit: false,
    albumId: "",
    commonDescrition: false,
    maxIndex: 0,
    redirectToAlbum: false,
    alertMessage: null,
    alertId: 0,
    alertType: "",
    isLoading: false
};

export const actionCreators = {
    initItems: (selectedPhotos, selectedAlbum) => ({ type: onInitType, selectedPhotos, selectedAlbum }),
    initAlbum: (selectedAlbum) => ({ type: onInitAlbumType, selectedAlbum }),

    onCommonDescriptionChanged: () => ({ type: setCommonDescriptionType }),

    onDrop: (pictures, pictureDataURLs, index) => ({ type: onDropType, pictures, pictureDataURLs, index }),
    onDropToAdd: (pictures, pictureDataURLs) => ({ type: onDropToAddType, pictures, pictureDataURLs }),

    onShotDateChange: (index, date) => ({ type: shotDateChangedType, index, date }),
    onPhotoNameEditChange: (index, event) => ({ type: photoNameChangedType, index, event }),

    onCameraChanged: (index, event) => ({ type: cameraChangedType, index, event }),
    onLensFocalLengthChanged: (index, event) => ({ type: lensFocalLengthChangedType, index, event }),

    onSaveChanges: () => ({ type: saveChangesType }),
    onPhotosUploadClick: () => ({ type: saveChangesType }),

    onRemoveSelectedPhoto: (index, event) => ({ type: removeSelectedPhotoType, index, event }),

    onEditPhotoClick: () => ({ type: onEditPhotoType }),
    onBeforeRedirect: () => ({ type: beforeRedirectType }),

    onRequestSuccess: () => ({ type: updateSuccessType }),
    onRequestError: (message) => ({ type: updateErrorType, message })
}

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === onInitType) {
        return {
            ...state,
            selectedPhotos: action.selectedPhotos,
            selectedAlbum: action.selectedAlbum,
            currentDate: new Date().getTime()
        };
    }

    if (action.type === onInitAlbumType) {
        return {
            ...state,
            selectedAlbum: action.selectedAlbum,
            selectedPhotos: []
        };
    }

    if (action.type === photoNameChangedType) {
        return {
            ...state,
            selectedPhotos: state.selectedPhotos.map((selectedPhoto, i) => ({
                ...selectedPhoto,
                photoName: i === action.index ? action.event.target.value : selectedPhoto.photoName
            }))
        };
    }

    if (action.type === removeSelectedPhotoType) {
        return {
            ...state,
            selectedPhotos: state.selectedPhotos.filter((item, index) => index !== action.index)
        };
    }

    if (action.type === shotDateChangedType) {
        return action.index >= 0 ?
            {
                ...state,
                selectedPhotos: state.selectedPhotos.map((selectedPhoto, i) => ({
                    ...selectedPhoto,
                    shotDate: i === action.index ? action.date : selectedPhoto.shotDate
                }))
            } :
            {
                ...state,
                selectedPhotos: state.selectedPhotos.map((selectedPhoto, i) => ({
                    ...selectedPhoto,
                    shotDate: action.date
                }))
            }
    }

    if (action.type === cameraChangedType) {
        return action.index >= 0 ? {
            ...state,
            selectedPhotos: state.selectedPhotos.map((selectedPhoto, i) => ({
                ...selectedPhoto,
                cameraModel: i === action.index ? action.event.target.value : selectedPhoto.cameraModel
            }))
        } :
            {
                ...state,
                selectedPhotos: state.selectedPhotos.map((selectedPhoto, i) => ({
                    ...selectedPhoto,
                    cameraModel: action.event.target.value
                }))
            }
    }


    if (action.type === lensFocalLengthChangedType) {

        return action.index >= 0 ? {
            ...state,
            selectedPhotos: state.selectedPhotos.map((selectedPhoto, i) => ({
                ...selectedPhoto,
                lensFocalLength: i === action.index ? action.event.target.value : selectedPhoto.lensFocalLength
            }))
        } :
            {
                ...state,
                selectedPhotos: state.selectedPhotos.map((selectedPhoto, i) => ({
                    ...selectedPhoto,
                    lensFocalLength: action.event.target.value
                }))
            }
    }

    if (action.type === removeSelectedPhotoType) {
        return {
            ...state,
            selectedPhotos: state.selectedPhotos.filter((item, index) => index !== action.index)
        };
    }

    if (action.type === onDropToAddType) {
        const actionDataURLs = action.pictureDataURLs.filter((item, index) => index >= state.maxIndex)
        const actionPictures = action.pictures.filter((item, index) => index >= state.maxIndex)
        const actionPhotos = action.pictures.filter((item, index) => index >= state.maxIndex).map((selectedPhoto, i) => ({
            photoUrl: actionDataURLs[i],
            picture: actionPictures[i],
            photoName: selectedPhoto.name,
            pictureDataURL: actionDataURLs[i]
        }))

        return {
            ...state,
            selectedPhotos: [...state.selectedPhotos, ...actionPhotos],
            maxIndex: state.maxIndex + actionPhotos.length
        };
    }

    if (action.type === onDropType) {
        return {
            ...state,
            selectedPhotos: state.selectedPhotos.map((selectedPhoto, i) => ({
                ...selectedPhoto,
                photoUrl: i === action.index ? action.pictureDataURLs[action.pictureDataURLs.length - 1] : selectedPhoto.photoUrl,
                photo: i === action.index ? action.pictures[action.pictures.length - 1] : selectedPhoto.photo,
                picture: i === action.index ? action.pictures[action.pictures.length - 1] : selectedPhoto.picture,
                pictureDataURL: i === action.index ? action.pictureDataURLs[action.pictureDataURLs.length - 1] : selectedPhoto.pictureDataURL
            }))
        };
    }

    if (action.type === saveChangesType) {
        return {
            ...state,
            postSaveChanges: true,
            isLoading: true
        };
    }

    if (action.type === setCommonDescriptionType) {
        return {
            ...state,
            commonDescrition: !state.commonDescrition
        }
    }

    if (action.type === onEditPhotoType) {
        return {
            ...state,
            redirectToEdit: true
        };
    }

    if (action.type === beforeRedirectType) {
        return {
            ...state,
            redirectToView: false,
            redirectToEdit: false,
            redirectToAlbum: false,
            alertMessage: null,
            alertId: 0,
            isLoading: false,
            selectedPhotos: [],
            maxIndex:0
        }
    }

    if (action.type === updateSuccessType) {
        return {
            ...state,
            postSaveChanges: false,
            redirectToAlbum: true,
            isLoading: false
        }
    }

    if (action.type === updateErrorType) {
        return {
            ...state,
            postSaveChanges: false,
            alertMessage: action.message,
            alertId: state.alertId + 1,
            alertType: "warning",
            isLoading: false
        }
    }

    return state;
}