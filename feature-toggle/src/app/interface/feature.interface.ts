import { FormControl } from "@angular/forms";
import { FeatureStatus , FeatureType} from "../enum/feature.enum";

export interface ILoginForm {
    email: FormControl<string | null>;
    password: FormControl<string | null>;
}

export interface ILoginAccept {
    email: string | null;
    password: string | null;
}

export interface ISignUpForm {
    fullName: FormControl<string | null>;
    email: FormControl<string | null>;
    password : FormControl<string | null>;
    confirmPassword : FormControl<string | null>;
}

export interface ISignUpAccept {
    fullName : string | null;
    email: string | null;
    password: string | null;
}

export interface IFeature {
    name: string;
    type: FeatureType;
    status: FeatureStatus;
}



export interface IBusiness {
    name: string;
    businessId: string;
    status: FeatureStatus;
  }