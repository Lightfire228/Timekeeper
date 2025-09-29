namespace Tk.Models;

public class Result<TOk, TErr> {

    private Result() {}


    public TOk?  OkVal  { get; private set; }
    public TErr? ErrVal { get; private set; }

    public static Result<TOk, TErr> Ok (TOk val) =>
        new() { OkVal = val }
    ;

    public static Result<TOk, TErr> Err(TErr val) =>
        new() { ErrVal = val }
    ;

    public TOk Ok() {
        if (OkVal == null) {
            throw new Exception("Result not ok");
        }

        return OkVal;
    }

    public TErr Err() {
        if (ErrVal == null) {
            throw new Exception("Result not err");
        }

        return ErrVal;
    }

    public bool IsOk() => OkVal != null;

    public Result<TOk, TErr> IfOk(Func<TOk, Result<TOk, TErr>> func) =>
        OkVal != null? func(OkVal) : this
    ;

    public Result<TOk, TErr> IfErr(Func<TOk, Result<TOk, TErr>> func) =>
        OkVal != null? func(OkVal) : this
    ;
}