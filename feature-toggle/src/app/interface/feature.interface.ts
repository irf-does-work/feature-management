import { FormControl } from "@angular/forms";
import { FeatureStatus , FeatureType} from "../enum/feature.enum";

export interface LoginForm {
    email: FormControl<string | null>;
    password: FormControl<string | null>;
}

export interface LoginAccept {
    email: string | null;
    password: string | null;
}

export interface SignUpForm {
    fullName: FormControl<string | null>;
    email: FormControl<string | null>;
    password : FormControl<string | null>;
    confirmPassword : FormControl<string | null>;
}

export interface Feature {
    name: string;
    type: FeatureType;
    status: FeatureStatus;
}