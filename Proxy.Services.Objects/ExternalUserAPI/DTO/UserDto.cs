namespace Proxy.Services.Objects.ExternalUserAPI.DTO
{
    public record UserDto(
     int Id,
     string Email,
     string First_name,
     string Last_name,
     string Avatar
 );

    public record PagedUserResponseDto(
        int Page,
        int Per_Page ,
        int Total,
        int Total_Pages,
        List<UserDto> Data
    );
}
